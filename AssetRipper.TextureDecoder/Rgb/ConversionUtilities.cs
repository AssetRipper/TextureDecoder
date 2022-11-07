namespace AssetRipper.TextureDecoder.Rgb
{
	internal static class ConversionUtilities
	{
		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		internal static TTo ConvertValue<TFrom, TTo>(TFrom value)
			where TFrom : unmanaged
			where TTo : unmanaged
		{
			if (typeof(TFrom) == typeof(TTo))
			{
				return Unsafe.As<TFrom, TTo>(ref value);
			}
			else if (typeof(TFrom) == typeof(sbyte))
			{
				byte converted = ChangeSign(Unsafe.As<TFrom, sbyte>(ref value));
				return ConvertUInt8<TTo>(converted);
			}
			else if(typeof(TFrom) == typeof(byte))
			{
				return ConvertUInt8<TTo>(Unsafe.As<TFrom, byte>(ref value));
			}
			else if (typeof(TFrom) == typeof(short))
			{
				ushort converted = ChangeSign(Unsafe.As<TFrom, short>(ref value));
				return ConvertUInt16<TTo>(converted);
			}
			else if (typeof(TFrom) == typeof(ushort))
			{
				return ConvertUInt16<TTo>(Unsafe.As<TFrom, ushort>(ref value));
			}
			else if (typeof(TFrom) == typeof(Half))
			{
				return ConvertHalf<TTo>(Unsafe.As<TFrom, Half>(ref value));
			}
			else if (typeof(TFrom) == typeof(float))
			{
				return ConvertSingle<TTo>(Unsafe.As<TFrom, float>(ref value));
			}
			else if (typeof(TFrom) == typeof(double))
			{
				return ConvertDouble<TTo>(Unsafe.As<TFrom, double>(ref value));
			}
			else
			{
				return ThrowOrReturnDefault<TTo>();
			}
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static TTo ConvertUInt8<TTo>(byte value) where TTo : unmanaged
		{
			if (typeof(TTo) == typeof(sbyte))
			{
				sbyte converted = ChangeSign(value);
				return Unsafe.As<sbyte, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(byte))
			{
				return Unsafe.As<byte, TTo>(ref value);
			}
			else if (typeof(TTo) == typeof(short))
			{
				short converted = ChangeSign(ConvertUInt8ToUInt16(value));
				return Unsafe.As<short, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(ushort))
			{
				ushort converted = ConvertUInt8ToUInt16(value);
				return Unsafe.As<ushort, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(Half))
			{
				Half converted = (Half)(value / (float)byte.MaxValue);
				return Unsafe.As<Half, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(float))
			{
				float converted = (float)(value / (float)byte.MaxValue);
				return Unsafe.As<float, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(double))
			{
				double converted = (double)(value / (double)byte.MaxValue);
				return Unsafe.As<double, TTo>(ref converted);
			}
			else
			{
				return ThrowOrReturnDefault<TTo>();
			}

			[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
			static ushort ConvertUInt8ToUInt16(byte value) => unchecked((ushort)((uint)value << 8));
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static TTo ConvertUInt16<TTo>(ushort value) where TTo : unmanaged
		{
			if (typeof(TTo) == typeof(sbyte))
			{
				sbyte converted = ChangeSign(ConvertUInt16ToUInt8(value));
				return Unsafe.As<sbyte, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(byte))
			{
				byte converted = ConvertUInt16ToUInt8(value);
				return Unsafe.As<byte, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(short))
			{
				short converted = ChangeSign(value);
				return Unsafe.As<short, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(ushort))
			{
				return Unsafe.As<ushort, TTo>(ref value);
			}
			else if (typeof(TTo) == typeof(Half))
			{
				Half converted = (Half)(value / (float)ushort.MaxValue);
				return Unsafe.As<Half, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(float))
			{
				float converted = (float)(value / (float)ushort.MaxValue);
				return Unsafe.As<float, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(double))
			{
				double converted = (double)(value / (double)ushort.MaxValue);
				return Unsafe.As<double, TTo>(ref converted);
			}
			else
			{
				return ThrowOrReturnDefault<TTo>();
			}

			[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
			static byte ConvertUInt16ToUInt8(ushort value) => unchecked((byte)((uint)value >> 8));
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static TTo ConvertHalf<TTo>(Half value) where TTo : unmanaged
		{
			if (typeof(TTo) == typeof(sbyte))
			{
				sbyte converted = ChangeSign(ClampUInt8((float)value * byte.MaxValue));
				return Unsafe.As<sbyte, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(byte))
			{
				byte converted = ClampUInt8((float)value * byte.MaxValue);
				return Unsafe.As<byte, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(short))
			{
				short converted = ChangeSign(ClampUInt16((float)value * ushort.MaxValue));
				return Unsafe.As<short, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(ushort))
			{
				ushort converted = ClampUInt16((float)value * ushort.MaxValue);
				return Unsafe.As<ushort, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(Half))
			{
				return Unsafe.As<Half, TTo>(ref value);
			}
			else if (typeof(TTo) == typeof(float))
			{
				float converted = (float)value;
				return Unsafe.As<float, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(double))
			{
				double converted = (double)value;
				return Unsafe.As<double, TTo>(ref converted);
			}
			else
			{
				return ThrowOrReturnDefault<TTo>();
			}
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static TTo ConvertSingle<TTo>(float value) where TTo : unmanaged
		{
			if (typeof(TTo) == typeof(sbyte))
			{
				sbyte converted = ChangeSign(ClampUInt8(value * byte.MaxValue));
				return Unsafe.As<sbyte, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(byte))
			{
				byte converted = ClampUInt8(value * byte.MaxValue);
				return Unsafe.As<byte, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(short))
			{
				short converted = ChangeSign(ClampUInt16(value * ushort.MaxValue));
				return Unsafe.As<short, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(ushort))
			{
				ushort converted = ClampUInt16(value * ushort.MaxValue);
				return Unsafe.As<ushort, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(Half))
			{
				Half converted = (Half)value;
				return Unsafe.As<Half, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(float))
			{
				return Unsafe.As<float, TTo>(ref value);
			}
			else if (typeof(TTo) == typeof(double))
			{
				double converted = (double)value;
				return Unsafe.As<double, TTo>(ref converted);
			}
			else
			{
				return ThrowOrReturnDefault<TTo>();
			}
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static TTo ConvertDouble<TTo>(double value) where TTo : unmanaged
		{
			if (typeof(TTo) == typeof(sbyte))
			{
				sbyte converted = ChangeSign(ClampUInt8(value * byte.MaxValue));
				return Unsafe.As<sbyte, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(byte))
			{
				byte converted = ClampUInt8(value * byte.MaxValue);
				return Unsafe.As<byte, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(short))
			{
				short converted = ChangeSign(ClampUInt16(value * ushort.MaxValue));
				return Unsafe.As<short, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(ushort))
			{
				ushort converted = ClampUInt16(value * ushort.MaxValue);
				return Unsafe.As<ushort, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(Half))
			{
				Half converted = (Half)value;
				return Unsafe.As<Half, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(float))
			{
				float converted = (float)value;
				return Unsafe.As<float, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(double))
			{
				return Unsafe.As<double, TTo>(ref value);
			}
			else
			{
				return ThrowOrReturnDefault<TTo>();
			}
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static T ThrowOrReturnDefault<T>() where T : unmanaged
		{
#if DEBUG
			throw new InvalidCastException();
#else
			return default; //exceptions prevent inlining
#endif
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static byte ClampUInt8(float x)
		{
			return byte.MaxValue < x ? byte.MaxValue : (x > byte.MinValue ? (byte)x : byte.MinValue);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static byte ClampUInt8(double x)
		{
			return byte.MaxValue < x ? byte.MaxValue : (x > byte.MinValue ? (byte)x : byte.MinValue);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static ushort ClampUInt16(float x)
		{
			return ushort.MaxValue < x ? ushort.MaxValue : (x > ushort.MinValue ? (ushort)x : ushort.MinValue);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static ushort ClampUInt16(double x)
		{
			return ushort.MaxValue < x ? ushort.MaxValue : (x > ushort.MinValue ? (ushort)x : ushort.MinValue);
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static byte ChangeSign(sbyte x)
		{
			unchecked
			{
				const uint SignBit = 0x80;
				return (byte)((uint)x ^ SignBit);
			}
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static sbyte ChangeSign(byte x)
		{
			unchecked
			{
				const uint SignBit = 0x80;
				return (sbyte)(x ^ SignBit);
			}
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static ushort ChangeSign(short x)
		{
			unchecked
			{
				const uint SignBit = 0x8000;
				return (ushort)((uint)x ^ SignBit);
			}
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static short ChangeSign(ushort x)
		{
			unchecked
			{
				const uint SignBit = 0x8000;
				return (short)(x ^ SignBit);
			}
		}
	}
}
