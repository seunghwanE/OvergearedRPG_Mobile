using UnityEngine;

namespace Trail2D
{
	public enum EmitMethod
	{
		Time = 0,
		Distance = 1
	}

	public enum LifeColorType
	{
		Solid = 0,
		Gradient = 1
	}

	public enum ScaleType
	{
		Static = 0,
		Curve = 1
	}

	[System.Serializable]
	public class TrailProperties
	{
		public EmitMethod emitMethod = EmitMethod.Time;
		public float emitDelay = 0.1f;
		public bool onlyWhenMoved = true;

		public float emitDistance = 1f;

		public float lifeTime = 1f;

		public bool overrideColor = false;
		public LifeColorType lifeColorType;
		public Color lifeColor = Color.white;
		public AnimationCurve alphaCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1), new Keyframe(1, 0) });
		public Gradient lifeGradient;

		public bool overrideScale = false;
		public ScaleType scaleType = ScaleType.Static;
		public Vector2 scale = Vector2.one;
		public AnimationCurve scaleCurveX = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1), new Keyframe(1, 1) });
		public AnimationCurve scaleCurveY = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1), new Keyframe(1, 1) });

		public int sortingLayer = 0;
		public int sortingOrder = 0;

		public bool overrideRenderer = false;
		public Material material = null;

		public TrailProperties()
		{
		}

		public TrailProperties(TrailProperties source)
		{
			this.emitMethod = source.emitMethod;
			this.emitDelay = source.emitDelay;
			this.emitDistance = source.emitDistance;

			this.lifeTime = source.lifeTime;

			this.overrideColor = source.overrideColor;
			this.lifeColorType = source.lifeColorType;
			this.lifeColor = source.lifeColor;

			this.lifeGradient = new Gradient();
			this.lifeGradient.alphaKeys = (GradientAlphaKey[])source.lifeGradient.alphaKeys.Clone();
			this.lifeGradient.colorKeys = (GradientColorKey[])source.lifeGradient.colorKeys.Clone();
			this.lifeGradient.mode = source.lifeGradient.mode;

			this.overrideScale = source.overrideScale;

			this.scaleType = source.scaleType;
			this.scale = source.scale;
			this.scaleCurveX = new AnimationCurve();
			this.scaleCurveX.keys = (Keyframe[])source.scaleCurveX.keys.Clone();
			this.scaleCurveY = new AnimationCurve();
			this.scaleCurveY.keys = (Keyframe[])source.scaleCurveY.keys.Clone();

			this.overrideRenderer = source.overrideRenderer;
			this.material = source.material;
		}
	}
}