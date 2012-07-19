namespace Remondo.Model
{
    public class Hotel : IEntity
    {
        public string Name { get; set; }
        public virtual City City { get; set; }

        #region IEntity Members

        public int Id { get; set; }

        #endregion
    }
}