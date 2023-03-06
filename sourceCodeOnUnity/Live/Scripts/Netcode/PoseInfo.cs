using Unity.Netcode;

internal struct PoseInfo : INetworkSerializable
{
	public BoneInfo[] Array;

	public PoseInfo(BoneInfo[] array)
	{
		Array = array;
	}

	public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
	{
		// Length
		int length = 0;
		if (!serializer.IsReader)
		{
			length = Array.Length;
		}

		serializer.SerializeValue(ref length);

		// Array
		if (serializer.IsReader)
		{
			Array = new BoneInfo[length];
		}

		for (int n = 0; n < length; ++n)
		{
			serializer.SerializeValue(ref Array[n]);
		}
	}
}
