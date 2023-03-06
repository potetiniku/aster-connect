using Cysharp.Threading.Tasks;
using UnityEngine;
using UniVRM10;

public class FacialExpression : MonoBehaviour
{
	private Vrm10RuntimeExpression expression;

	private void Start()
	{
		expression = GetComponent<Vrm10Instance>().Runtime.Expression;
		_ = LoopExpressionChange();
	}

	private async UniTask LoopExpressionChange()
	{
		while (true)
		{
			ExpressionKey[] expressions = new[]
			{
				ExpressionKey.Neutral,
				ExpressionKey.Relaxed,
				ExpressionKey.Happy
			};

			await SetExpression(
				expressions[Random.Range(0, expressions.Length)],
				Random.Range(2000, 4000));
		}
	}

	private async UniTask SetExpression(ExpressionKey key, int time_ms)
	{
		const int transitionFrame = 6;
		const float changePerFrame = 1f / transitionFrame;

		await Transit(true);
		await UniTask.Delay(time_ms);
		await Transit(false);

		async UniTask Transit(bool isPositive)
		{
			int bias = isPositive ? 1 : -1;

			for (int i = 0; i < transitionFrame; i++)
			{
				float currentWeight = expression.GetWeight(key);
				expression.SetWeight(key, currentWeight + changePerFrame * bias);
				await UniTask.NextFrame();
			}
		}
	}
}
