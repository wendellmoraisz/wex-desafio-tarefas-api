using DesafioAgendamentoTarefas.Controllers;
using DesafioAgendamentoTarefas.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace DesafioAgendamentoTarefasTests.Integrations.Controllers
{
    public class TarefasControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        public TarefasControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async Task ObterTarefaPorIdDeveRetornarOkQuandoTarefaExistir()
        {
            var idExistente = 2;

            var httpResponse = await _httpClient.GetAsync($"/Tarefas?id={idExistente}");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task ObterTodasTarefasDeveRetornarOk()
        {
            var httpResponse = await _httpClient.GetAsync("/Tarefas/ObterTodos");

            httpResponse.EnsureSuccessStatusCode();
            Assert.NotNull(httpResponse.Content);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task ObterTarefaPorTituloDeveRetornarOkQuandoTarefaExistir()
        {
            var tituloExistente = "Estudar C#";

            var httpResponse = await _httpClient.GetAsync($"/Tarefas/ObterPorTitulo?titulo={tituloExistente}");
            
            httpResponse.EnsureSuccessStatusCode();
            Assert.NotNull(httpResponse.Content);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task ObterTarefaPorDataDeveRetornarOkQuandoTarefaExistir()
        {
            var dataExistente = DateTime.Now.Date;

            var httpResponse = await _httpClient.GetAsync($"/Tarefas/ObterPorData?data={dataExistente}");

            httpResponse.EnsureSuccessStatusCode();
            Assert.NotNull(httpResponse.Content);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task ObterTarefasPorStatus_DeveRetornarOkQuandoTarefasExistirem()
        {
            var statusExistente = EStatusTarefa.Pendente;

            var httpResponse = await _httpClient.GetAsync($"/Tarefas/ObterPorStatus?status={statusExistente}");

            httpResponse.EnsureSuccessStatusCode();
            Assert.NotNull(httpResponse.Content);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CriarTarefaDeveRetornarSucesso()
        {
            var tarefaModel = new Tarefa
            {
                Data = DateTime.Now,
                Titulo = "Estudar C#",
                Descricao = "Assistir as aulas do Bootcamp Wex",
                Status = EStatusTarefa.Pendente
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(tarefaModel), Encoding.UTF8, "application/json");

            var httpResponse = await _httpClient.PostAsync("/Tarefas", content);

            Assert.NotNull(httpResponse.Content);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        }
    }
}
