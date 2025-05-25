using Nara.Game;
using Nara.Game.Model;
using Nara.Game.System;
using Sirenix.OdinInspector;
using System.Net.Http;
using UnityEngine;

public class TestHttp : MonoBehaviour
{
    [Button]
    public async void TestGetRequest()
    {
        var url = "https://jsonplaceholder.typicode.com/posts/1";
        //var url = "https://jsonplaceholderss.typicode.com/posts/1";
        var response = await GameApp.Interface.GetSystem<HttpService>().GetRequest(url);
        var data = await response.GetStringContentAsync();
        Debug.Log(data);
    }
    [Button]
    public async void TestPostRequest()
    {
        var url = "https://jsonplaceholder.typicode.com/posts";
        var content = new StringContent("{\"title\":\"foo\",\"body\":\"bar\",\"userId\":1}");
        var response = await GameApp.Interface.GetSystem<HttpService>().PostRequest(url, content);
        var data = await response.GetStringContentAsync();
        Debug.Log(data);
    }
    [Button]
    public async void TestPostAsXml()
    {
        var url = "https://jsonplaceholder.typicode.com/posts";
        var xml = new System.Xml.XmlDocument();
        var root = xml.CreateElement("root");
        var title = xml.CreateElement("title");
        title.InnerText = "foo";
        var body = xml.CreateElement("body");
        body.InnerText = "bar";
        var userId = xml.CreateElement("userId");
        userId.InnerText = "1";
        root.AppendChild(title);
        root.AppendChild(body);
        root.AppendChild(userId);
        xml.AppendChild(root);
        var response = await GameApp.Interface.GetSystem<HttpService>().PostAsXmlAsync(url, xml);
        var data = await response.GetStringContentAsync();
        Debug.Log(data);
    }
    public AgentModel agentModel;
}
