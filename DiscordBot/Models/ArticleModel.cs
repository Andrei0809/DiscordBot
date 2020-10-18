using System;

namespace DiscordBot.Models
{
    public class ArticleModel
    {
        public Response response { get; set; }
    }

    public class Response
    {
        public string status { get; set; }
        public string userTier { get; set; }
        public int total { get; set; }
        public int startIndex { get; set; }
        public int pageSize { get; set; }
        public int currentPage { get; set; }
        public int pages { get; set; }
        public string orderBy { get; set; }
        public Result[] results { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string type { get; set; }
        public string sectionId { get; set; }
        public string sectionName { get; set; }
        public DateTime webPublicationDate { get; set; }
        public string webTitle { get; set; }
        public string webUrl { get; set; }
        public string apiUrl { get; set; }
        public bool isHosted { get; set; }
        public string pillarId { get; set; }
        public string pillarName { get; set; }
    }
}
