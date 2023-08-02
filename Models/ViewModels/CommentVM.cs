namespace MoviesApplication.Models.ViewModels
{
    public class CommentVM
    {
        public Comment Comment { get; set; } = new Comment();
        public int MovieId { get; set; }  
    }
}
