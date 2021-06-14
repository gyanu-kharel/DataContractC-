using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace DataContract
{
    class Program
    {
        [DataContract]
        public class Result
        {
            [DataMember]
            public int id { get; set; }

            [DataMember]
            public string name { get; set; }

            [DataMember]
            public string username { get; set; }

            [DataMember]
            public Address address { get; set; }

            [DataMember]
            public string phone { get; set; }

            [DataMember]
            public string website { get; set; }

            [DataMember]
            public Company company { get; set; }

        }

        [DataContract]
        public class Address
        {
            [DataMember]
            public string street { get; set; }

            [DataMember]
            public string suite { get; set; }

            [DataMember]
            public string city { get; set; }

            [DataMember]
            public string zipcode { get; set; }  
            
            [DataMember]
            public Geo geo { get; set; }
        }
        
        [DataContract]
        public class Geo
        {         
            [DataMember]
            public string lat { get; set; }

            [DataMember]
            public string lang { get; set; }
        }

        [DataContract]
        public class Company
        {
            [DataMember]
            public string name { get; set; }

            [DataMember]
            public string catchPhrase { get; set; }   
            
            [DataMember]
            public string bs { get; set; }
        }


        [DataContract]
        public class Post
        {
            [DataMember]
            public int userid { get; set; }

            [DataMember]
            public string title { get; set; }

            [DataMember]
            public string body { get; set; }
        }

        public static Result DeSerialize(string json)
        {
            using (var stream = new MemoryStream(Encoding.Default.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(Result));
                return serializer.ReadObject(stream) as Result; 
            }
        }

        public static string Serialize(object obj)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(obj.GetType());
                serializer.WriteObject(stream, obj);
                return Encoding.UTF8.GetString(stream.GetBuffer());
            }
        }
        static void Main(string[] args)
        {
            var client = new HttpClient();
            var uri = "https://jsonplaceholder.typicode.com/users/1";

            var response = client.GetStringAsync(uri).Result;
            var data = DeSerialize(response);
            Console.WriteLine(data.company.name);


            var post = new Post()
            {
                userid = 10,
                title = "gyanu",
                body = " this is testing code"
            };

            var input = Serialize(post);
            var josn =  new System.Net.Http.StringContent(input, Encoding.UTF8, "application/json");

            var postUri = "https://jsonplaceholder.typicode.com/posts";
            var response2 = client.PostAsync(postUri, josn).Result;
                   
        }
    }
}
