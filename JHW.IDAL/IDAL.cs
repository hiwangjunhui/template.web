namespace JHW.IDAL
{
    public interface IDAL<T> : IDataReader<T>, IDataWriter<T>, IDataChanged<T> where T : class
    {
    }
}
