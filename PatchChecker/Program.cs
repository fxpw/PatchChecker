// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.InteropServices;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Reflection;

public static class HttpClientUtils
{
    public static async Task DownloadFileTaskAsync(this HttpClient client, Uri uri, string FileName)
    {
        using (var s = await client.GetStreamAsync(uri))
        {
            using (var fs = new FileStream(FileName, FileMode.CreateNew))
            {
                await s.CopyToAsync(fs);
            }
        }
    }
}


public class Program
{
    public static string pathToWow = "D:\\games\\sirus\\World of Warcraft Sirus";

    static HttpClient httpClient = new HttpClient();

    #region Check Patch whit md5
    [DllImport("PatchChecker.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern bool CheckPatch(string pathToFile, string md5);
    #endregion

    #region get md5 from file
    public delegate void ResponseDelegate(string s);
    [DllImport("PatchChecker.dll", EntryPoint = "GetMD5", CallingConvention = CallingConvention.StdCall)]
    /*
     *  GetMD5(pathToWow, pathToFileFromWowDir, s =>
        {
            Console.WriteLine(s);
        });
     */
    public static extern void GetMD5(string pathToFile, ResponseDelegate response);
    #endregion

    public static async Task CheckPatchAsync(Newtonsoft.Json.Linq.JToken info)
    {
        //Console.WriteLine(info);
        //var time = DateTime.Parse((string)info?["updated_at"], DateTimeFormatInfo.InvariantInfo);
        //var timeNow = DateTime.Now;
        var pathToFile = pathToWow + (string)info?["path"] + (string)info?["filename"];
        var urlForDownload = (string)info?["host"] + (string)info?["storage_path"];

        var isUpdated = CheckPatch(pathToFile, (string)info?["md5"]);
        if (!isUpdated)
        {
            using (HttpClient client = new System.Net.Http.HttpClient())
            {
                if (File.Exists(pathToFile))
                {
                    // If file found, delete it    
                    File.Delete(pathToFile);
                    //Console.WriteLine(pathToFile);
                }
                var uri = new Uri(urlForDownload);
                await httpClient.DownloadFileTaskAsync(uri, pathToFile);

            }
        }
        // тут можно накидывать в какую то переменную +1 чтобы чекать прогресс
        Console.WriteLine(pathToFile + " Checked be " + (isUpdated ? "updated " : "no updated"));
    }

    static async Task Main()
    {
        List<Task> tasks = new List<Task>();
        // запросики
        using (var client = new HttpClient())
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://51.15.228.31:8080/api/client/patches");
            var response = await client.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(pathToWow);
            Newtonsoft.Json.Linq.JObject obj = Newtonsoft.Json.Linq.JObject.Parse(content);
            // проверка патчей
            for (int i = 1; i < obj?["patches"]?.Count(); i++)
            {

                var info = obj?["patches"]?[i];
                //Console.WriteLine(info);

                tasks.Add(Task.Run(() => CheckPatchAsync(info)));

            }
            await Task.WhenAll(tasks);
            // обработка ответа от сервера
        }
        //using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://51.15.228.31:8080/api/client/patches");
        //using HttpResponseMessage response = await httpClient.SendAsync(request);
        
    }
}











