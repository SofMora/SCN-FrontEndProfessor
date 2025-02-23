namespace SCNProfessor.Models.Domain
{
    public class New
    {
        private int id;
        private string title;
        private string author;
        private string textNews;
        private DateTime dateNews;
        private string images;
        private string typeNews;

        public New()
        {

        }
        public New(int id, string title, string author, string textNews, DateTime dateNews, string images, string typeNews)
        {
            Id = id;
            Title = title;
            Author = author;
            TextNews = textNews;
            DateNews = dateNews;
            Images = images;
            TypeNews = typeNews;
        } 
        
        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Author { get => author; set => author = value; }
        public string TextNews { get => textNews; set => textNews = value; }
        public DateTime DateNews { get => dateNews; set => dateNews = value; }
        public string Images { get => images; set => images = value; }
        public string TypeNews { get => typeNews; set => typeNews = value; }
    }
}
