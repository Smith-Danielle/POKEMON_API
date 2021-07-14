using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PokemonAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string pokeURL = "https://pokeapi.co/api/v2/pokemon?limit=1000&offset=0/";
            //string original = "https://pokeapi.co/api/v2/pokemon/";


            //COMPLETE TODO create a new instance of HttpClient called client.
            var client = new HttpClient();

            //COMPLETE TODO use your client instance to get a response from the poke URL.
            var response = client.GetStringAsync(pokeURL).Result;
            //CW to get JSON file
            //Console.WriteLine(response);

            //COMPLETE TODO go to https://json2csharp.com and convert your json reponse to classes. Create a new class file in visual studio and paste the classes you created on the website.
            //In Root Class

            //COMPLETE TODO create a variable that = JsonConvert.DeserializeObject<YourRootClassGoesHere>(yourStringResponseGoesHere);
            var pokemon = JsonConvert.DeserializeObject<Root>(response);

            //COMPLETE TODO print the results from your
            //Prints Name and URL
            /*foreach (var item in pokemon.results)
            {
                Console.WriteLine(item.name);
                Console.WriteLine(item.url);
                Console.WriteLine("---------------------");
            }*/

            //COMPLETE TODO use the pokemon url above and change it to try and call your favorite pokemon.
            //COMPLETE TODO Use select token to try and grab a couple values from your pokemon and display them.

            //Create new urls for snorlax
            //foreach loop used to find url for snorlax
            /*foreach (var item in pokemon.results)
            {
                if (item.name == "snorlax")
                {
                    Console.WriteLine(item.url);
                }

            }*/
            string favoriteURL = "https://pokeapi.co/api/v2/pokemon/143/";
            var responseFav = client.GetStringAsync(favoriteURL).Result;
            //CW to get JSON file
            //Console.WriteLine(responseFav);

            var name = JObject.Parse(responseFav).SelectToken("species.name").ToString();
            Console.WriteLine(name);

            //Or
            var pokemonFav = JsonConvert.DeserializeObject<RootFav>(responseFav);
            foreach (var item in pokemonFav.forms)
            {
                Console.WriteLine(item.name);
            }

            //Let the user pick a pokemon. Used original url and select token for the pokemon they choose.
            Console.WriteLine("Please enter a pokemon to get their specs:");
            var character = Console.ReadLine();
            Console.Clear();

            foreach (var item in pokemon.results)
            {
                if (item.name == character)
                {
                    var extras = client.GetStringAsync(item.url).Result;
                    Console.WriteLine($"{character.ToUpper()} Specs");
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Height: {JObject.Parse(extras).SelectToken("height").ToString()}");
                    Console.WriteLine($"Weight: {JObject.Parse(extras).SelectToken("weight").ToString()}");
                    Console.WriteLine($"Abilities: {JObject.Parse(extras).SelectToken("abilities[0].ability.name").ToString()}, {JObject.Parse(extras).SelectToken("abilities[1].ability.name").ToString()}, {JObject.Parse(extras).SelectToken("abilities[2].ability.name").ToString()}");
                    




                }


            }
        }
    }
}
