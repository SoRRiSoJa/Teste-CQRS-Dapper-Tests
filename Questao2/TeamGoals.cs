using Newtonsoft.Json.Linq;

namespace Questao2
{
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
            JObject data = JObject.Parse(jsonString);

            if (data["data"] is not null)
            {
                return data["data"].Sum(match => int.Parse((string)match["team1goals"]));
            }
            return 0;
        }

    }
}
