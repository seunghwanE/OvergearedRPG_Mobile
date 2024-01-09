using UnityEngine;
using System.Collections;
using System;

namespace Trail2D
{
	public static class EaseUtility
	{
		// Adapted from source : http://www.robertpenner.com/easing/

		public static float Ease(float startValue, float endValue, float t, EaseType type, EaseDirection direction)
		{
			if(direction == EaseDirection.In) return EaseIn(startValue, endValue, t, type);
			else return EaseOut(startValue, endValue, t, type);
		}

		public static float EaseIn(float startValue, float endValue, float t, EaseType type)
		{
			return startValue + (endValue - startValue) * EaseIn(t, type);
		}

		public static float EaseOut(float startValue, float endValue, float t, EaseType type)
		{
			return startValue + (endValue - startValue) * EaseOut(t, type);
		}

		public static Vector2 Ease(Vector2 startValue, Vector2 endValue, float t, EaseType type, EaseDirection direction)
		{
			if(direction == EaseDirection.In) return EaseIn(startValue, endValue, t, type);
			else return EaseOut(startValue, endValue, t, type);
		}

		public static Vector2 EaseIn(Vector2 startValue, Vector2 endValue, float t, EaseType type)
		{
			return startValue + (endValue - startValue) * EaseIn(t, type);
		}

		public static Vector2 EaseOut(Vector2 startValue, Vector2 endValue, float t, EaseType type)
		{
			return startValue + (endValue - startValue) * EaseOut(t, type);
		}

		public static Vector3 Ease(Vector3 startValue, Vector3 endValue, float t, EaseType type, EaseDirection direction)
		{
			if(direction == EaseDirection.In) return EaseIn(startValue, endValue, t, type);
			else return EaseOut(startValue, endValue, t, type);
		}

		public static Vector3 EaseIn(Vector3 startValue, Vector3 endValue, float t, EaseType type)
		{
			return startValue + (endValue - startValue) * EaseIn(t, type);
		}

		public static Vector3 EaseOut(Vector3 startValue, Vector3 endValue, float t, EaseType type)
		{
			return startValue + (endValue - startValue) * EaseOut(t, type);
		}

		private static float EaseIn(double linearStep, EaseType type)
		{
			linearStep = Mathf.Clamp01((float)linearStep);

			switch(type)
			{
				case EaseType.Step: return linearStep < 0.5 ? 0 : 1;
				case EaseType.Linear: return (float)linearStep;
				case EaseType.Sine: return Sine.EaseIn(linearStep);
				case EaseType.Quadratic: return Power.EaseIn(linearStep, 2);
				case EaseType.Cubic: return Power.EaseIn(linearStep, 3);
				case EaseType.Quartic: return Power.EaseIn(linearStep, 4);
				case EaseType.Quintic: return Power.EaseIn(linearStep, 5);
				case EaseType.Elastic: return Elastic.EaseIn(linearStep);
				case EaseType.Bounce: return Bounce.EaseIn(linearStep);
				case EaseType.Back: return Back.EaseIn(linearStep);
			}
			throw new NotImplementedException();
		}

		private static float EaseOut(double linearStep, EaseType type)
		{
			linearStep = Mathf.Clamp01((float)linearStep);

			switch(type)
			{
				case EaseType.Step: return linearStep < 0.5 ? 0 : 1;
				case EaseType.Linear: return (float)linearStep;
				case EaseType.Sine: return Sine.EaseOut(linearStep);
				case EaseType.Quadratic: return Power.EaseOut(linearStep, 2);
				case EaseType.Cubic: return Power.EaseOut(linearStep, 3);
				case EaseType.Quartic: return Power.EaseOut(linearStep, 4);
				case EaseType.Quintic: return Power.EaseOut(linearStep, 5);
				case EaseType.Elastic: return Elastic.EaseOut(linearStep);
				case EaseType.Bounce: return Bounce.EaseOut(linearStep);
				case EaseType.Back: return Back.EaseOut(linearStep);
			}
			throw new NotImplementedException();
		}

		static class Sine
		{
			public static float EaseIn(double s)
			{
				return (float)Math.Sin(s * MathHelper.HalfPi - MathHelper.HalfPi) + 1;
			}
			public static float EaseOut(double s)
			{
				return (float)Math.Sin(s * MathHelper.HalfPi);
			}
			public static float EaseInOut(double s)
			{
				return (float)(Math.Sin(s * MathHelper.Pi - MathHelper.HalfPi) + 1) / 2;
			}
		}

		static class Power
		{
			public static float EaseIn(double s, int power)
			{
				return (float)Math.Pow(s, power);
			}
			public static float EaseOut(double s, int power)
			{
				var sign = power % 2 == 0 ? -1 : 1;
				return (float)(sign * (Math.Pow(s - 1, power) + sign));
			}
			public static float EaseInOut(double s, int power)
			{
				s *= 2;
				if(s < 1) return EaseIn(s, power) / 2;
				var sign = power % 2 == 0 ? -1 : 1;
				return (float)(sign / 2.0 * (Math.Pow(s - 2, power) + sign * 2));
			}
		}

		static class Elastic
		{
			public static float EaseIn(double s)
			{
				if(s >= 1) return 1;

				return (float)-(Math.Pow(2, 10 * (s -= 1)) * Math.Sin((s - 0.12) * (2 * Math.PI) / 0.3));
			}
			public static float EaseOut(double s)
			{
				if(s >= 1) return 1;

				return (float)(Math.Pow(2, -10 * s) * Math.Sin((s - 0.12) * (2 * Math.PI) / 0.4) + 1);
			}
			public static float EaseInOut(double s)
			{
				if(s >= 1) return 1;

				return (float)(-0.5 * (Math.Pow(2, 10 * (s -= 1)) * Math.Sin((s - 0.1125) * (2 * Math.PI) / 0.45)));
			}
		}

		static class Bounce
		{
			public static float EaseIn(double s)
			{
				return 1 - EaseOut(1 - s);
			}
			public static float EaseOut(double s)
			{
				if(s < (1 / 2.75))
					return (float)(7.5625 * s * s);
				else if(s < (2 / 2.75))
					return (float)(7.5625 * (s -= (1.5 / 2.75)) * s + .75);
				else if(s < (2.5 / 2.75))
					return (float)(7.5625 * (s -= (2.25 / 2.75)) * s + .9375);
				else
					return (float)(7.5625 * (s -= (2.625 / 2.75)) * s + .984375);
			}
			public static float EaseInOut(double s)
			{
				if(s < 0.5)
					return EaseIn(s * 2) * 0.5f;
				else
					return EaseOut(s * 2 - 1) * 0.5f + 0.5f;
			}
		}

		static class Back
		{
			public static float EaseIn(double s)
			{
				return (float)(s * s * ((1.70158 + 1) * s - 1.70158));
			}
			public static float EaseOut(double s)
			{
				return (float)((s -= 1) * s * ((1.70158 + 1) * s + 1.70158) + 1);
			}
			public static float EaseInOut(double s)
			{
				double e = 1.70158;
				if((s *= 2) < 1)
					return (float)(0.5 * (s * s * (((e *= (1.525)) + 1) * s - e)));
				return (float)(0.5 * ((s -= 2) * s * (((e *= (1.525)) + 1) * s + e) + 2));
			}
		}
	}

	public enum EaseDirection
	{
		In,
		Out
	}

	public enum EaseType
	{
		Step,
		Linear,
		Sine,
		Quadratic,
		Cubic,
		Quartic,
		Quintic,
		Elastic,
		Bounce,
		Back
	}

	public static class MathHelper
	{
		public const float Pi = (float)Math.PI;
		public const float HalfPi = (float)(Math.PI / 2);

		public static float Lerp(double from, double to, double step)
		{
			return (float)((to - from) * step + from);
		}
	}
}