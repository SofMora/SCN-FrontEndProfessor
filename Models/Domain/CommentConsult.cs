namespace SCNProfessor.Models.Domain
{
    public class CommentConsult
    {
        private int id;
        private int idConsult;
        private string descriptionComment;
        private int author;
        private DateTime dateComment;

        public CommentConsult()
        {
        }

        public CommentConsult(int id, int idConsult, string descriptionComment, int author, DateTime dateComment)
        {
            Id = id;
            IdConsult = idConsult;
            DescriptionComment = descriptionComment;
            Author = author;
            DateComment = dateComment;
        }

        public int Id { get => id; set => id = value; }
        public int IdConsult { get => idConsult; set => idConsult = value; }
        public string DescriptionComment { get => descriptionComment; set => descriptionComment = value; }
        public int Author { get => author; set => author = value; }
        public DateTime DateComment { get => dateComment; set => dateComment = value; }
    }
}
