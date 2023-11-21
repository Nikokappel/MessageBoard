using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class MBoardHttpClient : IMBoardService
{
    
    
    private readonly HttpClient client;


    public MBoardHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    
    public async Task CreateAsync(MBoardCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/MBoard", dto);
        if (!response.IsSuccessStatusCode)
        {
            string body = await response.Content.ReadAsStringAsync();
            throw new Exception(body);
        }
    }

    public async Task<ICollection<MessageBoard>> GetAsync(string? username, string? titleContains, string? body)
    {
        string query = ConstructQuery(username, titleContains, body);
        
        HttpResponseMessage response = await client.GetAsync("/MBoard"+query);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        ICollection<MessageBoard> messageBoards = JsonSerializer.Deserialize<ICollection<MessageBoard>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return messageBoards;
    }

    private static string ConstructQuery(string? username, string? titleContains, string? body)
    {
        string query = "";
        if (!string.IsNullOrEmpty(username))
        {
            query += $"?username={username}";
        }
       
        if (!string.IsNullOrEmpty(titleContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"title contains={titleContains}";
        }
        
        if (!string.IsNullOrEmpty(body))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"?body={body}";
        }
        
        return query;
    }
    public async Task<MBoardBasicDto> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/MBoard/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        MBoardBasicDto messageBoard = JsonSerializer.Deserialize<MBoardBasicDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return messageBoard;
    }
    public async Task UpdateAsync(MBoardUpdateDto dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("/MBoard", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
    
    public async Task DeleteAsync(int? id)
    {
        HttpResponseMessage response = await client.DeleteAsync($"MBoard/{id}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
    
    public async Task<IEnumerable<MessageBoard>> AsyncGetMessages(string? usernameContains = null)
    {
        string uri = "/MBoard";
        if (!string.IsNullOrEmpty(usernameContains))
        {
            uri += $"?username={usernameContains}";
        }
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<MessageBoard> users = JsonSerializer.Deserialize<IEnumerable<MessageBoard>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }
}