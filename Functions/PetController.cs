using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PetsMicroservice.Models;
using PetsMicroservice.Services;

namespace PetsMicroservice
{
    public class PetController
    {
        private readonly IPetService _petService;
        
        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        private async Task<IActionResult> GetResponse<T>(Func<Task<Response<T>>> func, ILogger log)
        {
            var response = await Task.Run(func);

            if (response.Success) return new OkObjectResult(response.Resource);

            log.LogInformation(response.Message);

            return new BadRequestObjectResult(new Error(response.Message));
        }

        /// <summary>
        /// Lists all Pets
        /// </summary>
        /// <returns>List all pets</returns>
        [FunctionName(nameof(ListAsync))]
        public async Task<IActionResult> ListAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Get), Route = "pets")] HttpRequest req,
            ILogger log)
        {
            return new OkObjectResult(await _petService.ListAsync());
        }

        /// <summary>
        /// Returns pet by identifier
        /// </summary>
        /// <param name="id">Pet identifier</param>
        /// <returns>Response for the request</returns>
        [FunctionName(nameof(FindByIdAsync))]
        public async Task<IActionResult> FindByIdAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Get), Route = "pets/{id}")] HttpRequest req, 
            int id,
            ILogger log)
        {
            return new OkObjectResult(await _petService.FindByIdAsync(id));
        }

        /// <summary>
        /// Saves a new Pet
        /// </summary>
        /// <returns>Response for the request</returns>
        [FunctionName(nameof(PostAsync))]
        public async Task<IActionResult> PostAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Post), Route = "pets")] HttpRequest req,
            ILogger log)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            
            var model = JsonConvert.DeserializeObject<Pet>(requestBody);

            return await GetResponse(() => _petService.SaveAsync(model), log);
        }
    }
}

