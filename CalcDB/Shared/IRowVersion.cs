namespace CalcDB.Shared
{
    public interface IRowVersion
    {
        public byte[] RowVersion { get; set; }
    }
}