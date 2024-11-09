namespace StudentService.Serialization
{
    public interface ISerializable
    {
        string[] ToCSV();

        void FromCSV(string[] values);
    }

}
