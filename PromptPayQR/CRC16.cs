namespace PromptPayQR
{
    public class CRC16
    {
        private const ushort polynomial = 0x1021;
        private readonly ushort initialValue = 0xffff;
        private readonly ushort[] table = new ushort[256];

        public CRC16()
        {
            ushort temp, a;
            for (var i = 0; i < table.Length; i++)
            {
                temp = 0;
                a = (ushort) (i << 8);
                for (var j = 0; j < 8; j++)
                {
                    if (((temp ^ a) & 0x8000) != 0)
                        temp = (ushort) ((temp << 1) ^ polynomial);
                    else
                        temp <<= 1;
                    a <<= 1;
                }
                table[i] = temp;
            }
        }

        public ushort ComputeCheckSum(byte[] bytes)
        {
            var crc = initialValue;
            for (var i = 0; i < bytes.Length; i++)
                crc = (ushort) ((crc << 8) ^ table[(crc >> 8) ^ (0xff & bytes[i])]);
            return crc;
        }

        public byte[] ComputeChecksumBytes(byte[] bytes)
        {
            var crc = ComputeCheckSum(bytes);
            return new[] {(byte) (crc >> 8), (byte) (crc & 0x00ff)};
        }
    }
}