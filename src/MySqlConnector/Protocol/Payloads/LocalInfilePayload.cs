using System.Text;
using MySqlConnector.Protocol.Serialization;

namespace MySqlConnector.Protocol.Payloads
{
	// See https://dev.mysql.com/doc/internals/en/com-query-response.html#local-infile-request
	internal sealed class LocalInfilePayload
	{
		public const byte Signature = 0xFB;

		public string FileName { get; }

		public static LocalInfilePayload Create(PayloadData payload)
		{
			var reader = new ByteArrayReader(payload.ArraySegment);
			reader.ReadByte(Signature);
			var fileName = Encoding.UTF8.GetString(reader.ReadByteString(reader.BytesRemaining));
			return new LocalInfilePayload(fileName);
		}

		private LocalInfilePayload(string fileName)
		{
			FileName = fileName;
		}
	}
}
