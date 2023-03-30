// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.InteropServices;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;

public struct Haash
{
    public string hash { get; set; }

    //public  int Count { get; set; }
}

public class Program
{
    // Uncomment the following line to resolve.
    static HttpClient httpClient = new HttpClient();

    [DllImport("PatchChecker.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern bool CheckPatch(string pathToFile, string md5);

    public delegate void ResponseDelegate(string s);
    [DllImport("PatchChecker.dll", EntryPoint = "GetMD5", CallingConvention = CallingConvention.StdCall)]
    /*
     *  GetMD5(pathToWow, pathToFileFromWowDir, s =>
        {
            Console.WriteLine(s);
        });
     */
    public static extern void GetMD5(string path, string name, ResponseDelegate response);

    public static string pathToWow = "D:\\games\\sirus\\World of Warcraft Sirus";
    static async Task Main(){
        await Task.Run(async () =>
        {
            // определяем данные запроса
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://51.15.228.31:8080/api/client/patches");

            // получаем ответ
            using HttpResponseMessage response = await httpClient.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(pathToWow);
            Newtonsoft.Json.Linq.JObject obj = Newtonsoft.Json.Linq.JObject.Parse(content);
            for (int i = 1; i < obj?["patches"]?.Count(); i++){
                
                var info = obj?["patches"]?[i];
                Console.WriteLine(info);
                //Console.WriteLine(obj?["patches"]?[i]?["filename"]);
                //Console.WriteLine(obj?["patches"]?[i]?["path"]);
                var time  = DateTime.Parse((string)info["updated_at"], DateTimeFormatInfo.InvariantInfo);
                var timeNow = DateTime.Now;
                if (time > timeNow)
                {
                    Console.WriteLine(time);
                    var cBool = CheckPatch(pathToWow + info?["path"] + info?["filename"], (string)info?["md5"]);
                    //await Task.Run(async () =>
                    //{
                    //await Task.Run(() => { Console.WriteLine(); });
                    //});
                    Console.WriteLine("----------------------");
                }


            }
        });
    }
}











