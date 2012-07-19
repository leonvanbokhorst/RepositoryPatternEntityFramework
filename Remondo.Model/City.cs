namespace Remondo.Model
{
    public class City : IEntity
    {
        public string Name { get; set; }

        #region IEntity Members

        public int Id { get; set; }

        #endregion
    }
}