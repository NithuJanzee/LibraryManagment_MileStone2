namespace LibraryManagment.Entity
{
    public class History
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }       
        public Guid BookId { get; set; }             

        public string Status { get; set; }           

        public DateTime? RequestedDate { get; set; }  
        public DateTime? LendedDate { get; set; }    
        public DateTime? DueDate { get; set; }       
        public DateTime? ReturnedDate { get; set; }    


        public virtual Users User { get; set; }        
        public virtual Book Book { get; set; }         
    }
}
