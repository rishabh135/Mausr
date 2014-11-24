﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Mausr.Core {
	public static class MathNetExtensions {

		/// <summary>
		/// Packs given matrices to the array.
		/// </summary>
		public static Vector<double> Pack(this Matrix<double>[] matrices) {

			int size = 0;
			for (int i = 0; i < matrices.Length; ++i) {
				size += matrices[i].RowCount * matrices[i].ColumnCount;
			}

			var result = new DenseVector(size);
			int globalIndex = 0;

			for (int i = 0; i < matrices.Length; ++i) {
				var m = matrices[i];
				int rows = m.RowCount;
				int cols = m.ColumnCount;

				for (int c = 0; c < cols; ++c) {
					for (int r = 0; r < rows; ++r) {
						result[globalIndex++] = m[r, c];
					}
				}
			}

			return result;
		}

		/// <summary>
		/// Packs given matrices to the array.
		/// </summary>
		public static void Unpack(this Vector<double> packedMatrices, Matrix<double>[] resultMatrices) {
			Contract.Requires(packedMatrices.Count == resultMatrices.Sum(m => m.RowCount * m.ColumnCount));

			int globalIndex = 0;
			for (int i = 0; i < resultMatrices.Length; ++i) {
				var m = resultMatrices[i];
				int rows = m.RowCount;
				int cols = m.ColumnCount;

				for (int c = 0; c < cols; ++c) {
					for (int r = 0; r < rows; ++r) {
						m[r, c] = packedMatrices[globalIndex++];
					}
				}
			}
		}

	}
}