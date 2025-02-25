namespace SCNProfessor.Models.Domain
{
    public class Comment
    {
        private int id;
        private int idNews;
        private string description;
        private int author; 
        private DateTime commentDate;

       
        public Comment() { }

      
        public Comment(int id, int idNews, string description, int author, DateTime commentDate)
        {
            Id = id;
            IdNews = idNews;
            Description = description;
            Author = author;
            CommentDate = commentDate;
            
          
        }

        public int Id { get => id; set => id = value; }
        public int IdNews { get => idNews; set => idNews = value; }
        public string Description { get => description; set => description = value; }
        public int Author { get => author; set => author = value; }
        public DateTime CommentDate { get => commentDate; set => commentDate = value; }
    }
}
