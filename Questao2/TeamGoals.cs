using Newtonsoft.Json;

namespace Questao2;

public  class TeamGoals
{
    private readonly string _url = "https://jsonmock.hackerrank.com/api/football_matches";
    private readonly HttpClient _client;
    public TeamGoals()
    {
        _client = new HttpClient();
    }
    public async Task<int> GetTotalGoals(string team, int year)
    {
        HttpResponseMessage response = await _client.GetAsync($"{_url}?year={year}&team1={team}");
        string jsonString = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(jsonString))
        {
            return 0;
        }
        Root result = JsonConvert.DeserializeObject<Root>(jsonString);

        if (result is not null && result.data.Any())
        {
            return result.data.Sum(match => int.Parse((string)match.team1goals));
        }
        return 0;
    }

}
