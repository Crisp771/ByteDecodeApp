public class ConvertStringToByteArrayService: IConvertStringToByteArrayService {
    public byte[] ConvertString(string byteString) {
        if (String.IsNullOrEmpty(byteString)) throw new ArgumentException();
        if (byteString.Length % 2 > 0) throw new ArgumentException();
        var bytes = new List<byte>();
        for (int i = 0; i < byteString.Length; i += 2)
        {
            bytes.Add(Convert.ToByte(byteString.Substring(i, 2), 16));
        }
        return bytes.ToArray();
    }
}