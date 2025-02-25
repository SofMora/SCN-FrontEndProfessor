namespace SCNProfessor.Models.Domain
{
    public class Professor
    {
        private int id;
        private string name;
        private string lastName;
        private string email;
        private string userName;
        private string password;
        private string description;
        private string photo;
        private string socialLink;
        private bool statusProfessor;

        public Professor()
        {
        }

        public Professor(int id, string name, string lastName, string email, string userName, string password, string description, string photo, string socialLink, bool statusProfessor)
        {
            this.Id = id;
            this.Name = name;
            this.LastName = lastName;
            this.Email = email;
            this.UserName = userName;
            this.Password = password;
            this.Description = description;
            this.Photo = photo;
            this.SocialLink = socialLink;
            this.StatusProfessor = statusProfessor;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string Description { get => description; set => description = value; }
        public string Photo { get => photo; set => photo = value; }
        public string SocialLink { get => socialLink; set => socialLink = value; }
        public bool StatusProfessor { get => statusProfessor; set => statusProfessor = value; }
    }
}
