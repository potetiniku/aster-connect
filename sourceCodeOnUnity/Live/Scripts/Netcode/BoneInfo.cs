using Unity.Netcode;
using UnityEngine;

internal struct BoneInfo : INetworkSerializable
{
	public HumanBodyBones bone;
	public float targetX;
	public float targetY;
	public float targetZ;
	public float targetW;
	public float alpha;

	public BoneInfo(HumanBodyBones bone, Quaternion target, float alpha)
	{
		this.bone = bone;
		this.targetX = target.x;
		this.targetY = target.y;
		this.targetZ = target.z;
		this.targetW = target.w;
		this.alpha = alpha;
	}

	public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
	{
		serializer.SerializeValue(ref bone);
		serializer.SerializeValue(ref targetX);
		serializer.SerializeValue(ref targetY);
		serializer.SerializeValue(ref targetZ);
		serializer.SerializeValue(ref targetW);
		serializer.SerializeValue(ref alpha);
	}
}
