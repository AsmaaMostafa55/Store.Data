namespace Store.Data.Entities
{
    public class Baseentity<T>
    {
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
