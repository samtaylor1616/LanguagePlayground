﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;

namespace WebAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Respository Reporter");

            var endpoint = "https://api.github.com/orgs/dotnet/repos";
            var streamTask = client.GetStreamAsync(endpoint);
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);

            foreach (var repo in repositories)
                Console.WriteLine(repo.name);

        }
    }
}
