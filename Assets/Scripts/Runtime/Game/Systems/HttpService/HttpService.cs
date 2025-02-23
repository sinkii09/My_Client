using Nara.Core.Architecture;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace Nara.Game.System
{
    public class HttpConfiguration
    {
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(10);
        public string BaseUrl { get; set; }

    }
    public class HttpService : BaseSystem, IDisposable
    {
        private HttpClient _client;
        private TimeSpan _timeout = TimeSpan.FromSeconds(10);

        public event Action OnRequestSuccess;
        public event Action OnRequestFailed;
        public event Action OnRequestCancelled;
        public event Action OnRequestStarted;
        protected override bool OnInit()
        {
            _client = new HttpClient()
            {
                Timeout = _timeout
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
            return true;
        }
        protected override void OnTerminate()
        {
            Dispose();
        }
        private async Task<HttpResponseMessage> RetrieveRequest(Func<CancellationToken, Task<HttpResponseMessage>> action, CancellationToken token)
        {
            var delay = TimeSpan.FromSeconds(1);
            while (true)
            {
                try
                {
                    OnRequestStarted?.Invoke();
                    var result = await action(token);
                    OnRequestSuccess?.Invoke();
                    return result;
                }
                catch (TaskCanceledException ex) when (token.IsCancellationRequested)
                {
                    OnRequestCancelled?.Invoke();
                    Debug.LogWarning($"Request was cancelled. {ex}");
                    return default;
                }
                catch (TaskCanceledException ex)
                {
                    OnRequestFailed?.Invoke();
                    Debug.LogWarning($"Request timed out. {ex}");
                    return default;
                }
                catch (Exception e)
                {
                    if (delay < _timeout)
                    {
                        Debug.LogWarning($"Request failed, retrying in {delay.TotalSeconds} seconds. {e}");
                        await Task.Delay(delay, token);
                        delay = TimeSpan.FromSeconds(Math.Min(delay.TotalSeconds * 2, _timeout.TotalSeconds));
                    }
                    else
                    {
                        Debug.LogWarning($"Request failed after retries. {e}");
                        OnRequestFailed?.Invoke();
                        return default;
                    }
                }
            }
        }
        public async Task<HttpResponseMessage> GetRequest(string url, CancellationToken token = default)
        {
            return await RetrieveRequest((ct) => _client.GetAsync(url, ct), token);
        }
        public async Task<HttpResponseMessage> PostRequest(string url, HttpContent content, CancellationToken token = default)
        {
            return await RetrieveRequest((ct) => _client.PostAsync(url, content, ct), token);
        }
        public async Task<HttpResponseMessage> PostAsJsonAsync(string url, object data, CancellationToken token = default)
        {
            var json = JsonUtility.ToJson(data);
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await PostRequest(url, content, token);
        }
        public async Task<HttpResponseMessage> PostAsXmlAsync(string url, object data, CancellationToken token = default)
        {
            var xml = new StringWriter();
            var serializer = new XmlSerializer(data.GetType());
            serializer.Serialize(xml, data);
            var content = new StringContent(xml.ToString());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            return await PostRequest(url, content, token);
        }
        public async Task<HttpResponseMessage> PostAsBytesAsync(string url, byte[] data, CancellationToken token = default)
        {
            var content = new ByteArrayContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return await PostRequest(url, content, token);
        }
        public void SetAuthorizationHeader(string scheme, string parameter)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, parameter);
        }
        public void Dispose()
        {
            _client?.Dispose();
            Debug.Log("HttpService disposed.");
        }
    }
    public static class HttpExtension
    {
        public static async Task<string> GetStringContentAsync(this HttpResponseMessage response)
        {
            try
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                Debug.LogWarning($"HTTP request error: {ex.Message}");
                return default;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Unexpected error: {ex.Message}");
                return default;
            }
        }
        public static async Task<byte[]> GetBytesContentAsync(this HttpResponseMessage response)
        {
            try
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (HttpRequestException ex)
            {
                Debug.LogWarning($"HTTP request error: {ex.Message}");
                return default;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Unexpected error: {ex.Message}");
                return default;
            }
        }
        public static async Task<T> DeserializeResponseAsync<T>(this HttpResponseMessage response)
        {
            try
            {
                response.EnsureSuccessStatusCode();
                var contentType = response.Content.Headers.ContentType.MediaType;

                switch (contentType)
                {
                    case "application/json":
                        var jsonString = await response.Content.ReadAsStringAsync();
                        return JsonUtility.FromJson<T>(jsonString);
                    case "application/xml":
                        var xmlString = await response.Content.ReadAsStringAsync();
                        using (var reader = new StringReader(xmlString))
                        {
                            var serializer = new XmlSerializer(typeof(T));
                            return (T)serializer.Deserialize(reader);
                        }
                    case "application/octet-stream":
                        var byteArray = await response.Content.ReadAsByteArrayAsync();
                        return (T)(object)byteArray;
                    default:
                        Debug.LogWarning($"Unsupported content type: {contentType}");
                        return default;
                }
            }
            catch (HttpRequestException ex)
            {
                Debug.LogWarning($"HTTP request error: {ex.Message}");
                return default;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Unexpected error: {ex.Message}");
                return default;
            }
        }
    }
}