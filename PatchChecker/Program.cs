// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.InteropServices;
using System;
using System.Threading;
using System.Threading.Tasks;


public class Program
{
    // Uncomment the following line to resolve.
    static HttpClient httpClient = new HttpClient();
    [DllImport("PatchChecker.dll", CallingConvention = CallingConvention.Cdecl)]
    //public static extern int CheckPatch(string path, string name, string md5);
    //public static extern string CheckPatch(string path, string name, string md5);
    public static extern bool CheckPatch(string path, string name, string md5);

    [DllImport("PatchChecker.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool DownloadPatch(string path, string name);
    [DllImport("PatchChecker.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern string GetMD5(string path, string name);
    public static string path = "D:\\games\\sirus\\World of Warcraft Sirus";
    static async Task Main(){
        await Task.Run(async () =>
        {
            // определяем данные запроса
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://51.15.228.31:8080/api/client/patches");

            // получаем ответ
            using HttpResponseMessage response = await httpClient.SendAsync(request);

            // содержимое ответа
            Console.WriteLine("\nContent");
            string content = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(content);
            //Console.WriteLine("\nNew");
            var PathWow = "D:\\games\\sirus\\World of Warcraft Sirus";
            Console.WriteLine(PathWow);
            Newtonsoft.Json.Linq.JObject obj = Newtonsoft.Json.Linq.JObject.Parse(content);
            for (int i = 1; i < obj?["patches"]?.Count(); i++)
            {
                //Console.WriteLine(obj?["patches"]?[i]);
                //Console.WriteLine(obj?["patches"]?[i]?["filename"]);
                //Console.WriteLine(obj?["patches"]?[i]?["path"]);
                //Console.WriteLine(obj?["patches"]?[i]?["size"]);
                //Console.WriteLine(obj?["patches"]?[i]?["md5"]);
                ////Console.WriteLine(obj?["patches"]?[i]["host"]);
                //Console.WriteLine(obj?["patches"]?[i]?["updated_at"]);
                ////Console.WriteLine(obj?["patches"]?[i]["storage_path"]);
                //var downloadLink = (string)obj?["patches"]?[i]?["host"] + (string)obj?["patches"]?[i]?["storage_path"];
                //Console.WriteLine(downloadLink, "link");
                //Console.WriteLine("----------------------");
                if ((string)obj?["patches"]?[i]?["filename"] == "patch-ruRU-8.mpq")
                {
                    Console.WriteLine((string)obj?["patches"]?[i]?["filename"]);
                    Console.WriteLine(obj?["patches"]?[i]?["md5"]);
                    Console.WriteLine(CheckPatch("da", "./patch-ruRU-8.mpq", (string)obj?["patches"]?[i]?["md5"]));
                    //Console.WriteLine((string)GetMD5("da", "./patch-ruRU-8.mpq"));
                }



            }
            
            //Console.WriteLine(obj.ToString());
            //var objects = JsonConvert.DeserializeObject<List<string>>(content);
            //for(int i = 0; i < objects.Count(); i++)
            //{
            //Console.WriteLine(objects[i]);
            //}
            //Console.WriteLine(CheckPatch(path, "da"));
        });
        //Console.WriteLine(CheckPatch("da", "./patch-ruRU-8.mpq"));
        
        //_ = DLL.GetJson(httpClient);
        //var task = DLL.GetJson(httpClient);
        //var result = task.WaitAndUnwrapException();
        //var result = AsyncContext.Run(MyAsyncMethod);

    }
}











