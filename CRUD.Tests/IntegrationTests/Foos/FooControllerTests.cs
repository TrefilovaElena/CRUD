using CRUD.DTO.Abstract.Filter;
using CRUD.DTO.In.Foos;
using CRUD.DTO.Out.Foos;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace CRUD.Tests.IntegrationTests.Foos
{
    public class FooControllerTests
    {
        private string _uriString => $"http://localhost:5010/api/Foos";
        private Uri _resourceUri => new Uri(_uriString);
        private HttpClient _client;

        private string _content = "FooTest1";
        private string _newContent = $"New FooTest1";
        private long _actualId = 0;

        [OneTimeSetUp]
        public async Task Setup()
        {
            _client = new HttpClient { BaseAddress = _resourceUri };
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _client?.Dispose();
        }


        /// <summary>
        /// Добавление 
        /// </summary>
        /// <returns></returns>
        [Test, Order(10)]
        public async Task CreateFoo_ReturnSuccess()
        {
            var input = new FooAddInDto
            {                  
                Title = _content,
                FooFooId = 1
            };

            var requestBody = JsonContent.Create(input);
            using var response = await _client.PostAsync("", requestBody);

            Assert.That(response.IsSuccessStatusCode, Is.True);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FooOutDto>(content);
            Assert.That(result.Title, Is.EqualTo(_content));
            _actualId = result.Id;
        }



        /// <summary>
        /// Получение Foo
        /// </summary>
        /// <returns></returns>
        [Test, Order(20)]
        public async Task Get_ReturnSuccess()
        {
            using var response = await _client.GetAsync($"{_uriString}/{_actualId}");

            Assert.That(response.IsSuccessStatusCode, Is.True);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FooOutDto>(content);
            Assert.That(result.Title, Is.EqualTo(_content));

        }
   


        [Test, Order(30)]
        public async Task FilterByName_Returns1()
        {
            using var response = await _client.GetAsync($"{_uriString}/Filter?Title={_content}");

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.That(response.IsSuccessStatusCode, Is.True);

            var filterResult = JsonConvert.DeserializeObject<FilterOutDto<FooOutDto>>(result);
            Assert.That(filterResult.Items, Has.Count.EqualTo(1));
        }


        /// <summary>
        /// Обновление 
        /// </summary>
        /// <returns></returns>
        [Test, Order(40)]
        public async Task Update_ReturnSuccess()
        { 
            var input = new FooUpdateInDto
            {
                Title = _newContent,
                FooFooId = 2
            };
            var requestBody = JsonContent.Create(input);
            using var response = await _client.PatchAsync($"{_uriString}/{_actualId}", requestBody);

            Assert.That(response.IsSuccessStatusCode, Is.True);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FooOutDto>(content);
            Assert.That(result.Title, Is.EqualTo(_newContent));
        }

    

        /// <summary>
        /// Удаление
        /// </summary>
        /// <returns></returns>
        [Test, Order(50)]
        public async Task Delete_ReturnSuccess()
        {
            using var response = await _client.DeleteAsync($"{_uriString}/{_actualId}");
            Assert.That(response.IsSuccessStatusCode, Is.True);

            using var responseDelete = await _client.GetAsync($"{_uriString}/{_actualId}");
            var content = await responseDelete.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FooOutDto>(content);
            Assert.That(result.IsDeleted, Is.EqualTo(true));
        }

        /// <summary>
        /// Полное удаление
        /// </summary>
        /// <returns></returns>
        [Test, Order(60)]
        public async Task ForceDelete_ReturnSuccess()
        {
            using var response = await _client.DeleteAsync($"{_uriString}/{_actualId}?isForced=true");
            Assert.That(response.IsSuccessStatusCode, Is.True);

            using var responseDelete = await _client.GetAsync($"{_uriString}/{_actualId}");
            Assert.That((int)responseDelete.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        }
    }
}