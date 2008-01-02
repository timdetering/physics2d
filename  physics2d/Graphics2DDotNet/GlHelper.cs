#region MIT License
/*
 * Copyright (c) 2005-2008 Jonathan Mark Porter. http://physics2d.googlepages.com/
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to deal 
 * in the Software without restriction, including without limitation the rights to 
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of 
 * the Software, and to permit persons to whom the Software is furnished to do so, 
 * subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be 
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
 * PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE 
 * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 */
#endregion

#if UseDouble
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using AdvanceMath;
using Tao.OpenGl;
namespace Graphics2DDotNet
{
    public static class GlHelper
    {
#if UseDouble
        public const int GlScalar = Gl.GL_DOUBLE;
#else
        public const int GlScalar = Gl.GL_FLOAT;
#endif

        public static void GlVertex(Vector2D vertex)
        {
#if UseDouble
            Gl.glVertex2d(vertex.X, vertex.Y);
#else
            Gl.glVertex2f(vertex.X, vertex.Y);
#endif
        }
        public static void GlVertex(Vector3D vertex)
        {
#if UseDouble
            Gl.glVertex3d(vertex.X, vertex.Y, vertex.Z);
#else
            Gl.glVertex3f(vertex.X, vertex.Y, vertex.Z);
#endif
        }
        public static void GlVertex(Vector4D vertex)
        {
#if UseDouble
            Gl.glVertex4d(vertex.X, vertex.Y, vertex.Z,vertex.W);
#else
            Gl.glVertex4f(vertex.X, vertex.Y, vertex.Z, vertex.W);
#endif
        }

        public static void GlLoadMatrix(Matrix4x4 matrix)
        {
            Scalar[] array = new Scalar[16];
            Matrix4x4.CopyTranspose(ref matrix, array);
            GlLoadMatrix(array);
        }
        public static void GlLoadMatrix(Matrix3x3 matrix)
        {
            Scalar[] array = new Scalar[16];
            Matrix3x3.Copy2DToOpenGlMatrix(ref matrix, array);
            GlLoadMatrix(array);
        }
        public static void GlLoadMatrix(Matrix2x3 matrix)
        {
            Scalar[] array = new Scalar[16];
            Matrix2x3.Copy2DToOpenGlMatrix(ref matrix, array);
            GlLoadMatrix(array);
        }
        public static void GlLoadMatrix(Scalar[] array)
        {
#if UseDouble
            Gl.glLoadMatrixd(array);
#else
            Gl.glLoadMatrixf(array);
#endif
        }

        public static void GlMultMatrix(Matrix4x4 matrix)
        {
            Scalar[] array = new Scalar[16];
            Matrix4x4.CopyTranspose(ref matrix, array);
            GlMultMatrix(array);
        }
        public static void GlMultMatrix(Matrix3x3 matrix)
        {
            Scalar[] array = new Scalar[16];
            Matrix3x3.Copy2DToOpenGlMatrix(ref matrix, array);
            GlMultMatrix(array);
        }
        public static void GlMultMatrix(Matrix2x3 matrix)
        {
            Scalar[] array = new Scalar[16];
            Matrix2x3.Copy2DToOpenGlMatrix(ref matrix, array);
            GlMultMatrix(array);
        }
        public static void GlMultMatrix(Scalar[] array)
        {
#if UseDouble
            Gl.glMultMatrixd(array);
#else
            Gl.glMultMatrixf(array);
#endif
        }

        public static void GlBufferData(int target, Array array, int size, int usage)
        {
            GCHandle handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            try
            {
                Gl.glBufferData(
                    target,
                    new IntPtr(size),
                    handle.AddrOfPinnedObject(),
                    usage);
            }
            finally
            {
                handle.Free();
            }
        }
        public static void GlBufferDataARB(int target, Array array,int size, int usage)
        {
            GCHandle handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            try
            {
                Gl.glBufferDataARB(
                    target,
                    new IntPtr(size),
                    handle.AddrOfPinnedObject(),
                    usage);
            }
            finally
            {
                handle.Free();
            }
        }

        public static void GlColor3(Scalar red, Scalar green, Scalar blue)
        {
#if UseDouble
            Gl.glColor3d(red, green, blue);
#else
            Gl.glColor3f(red, green, blue);
#endif
        }
        public static void GlColor4(Scalar red, Scalar green, Scalar blue, Scalar alpha)
        {
#if UseDouble
            Gl.glColor4d(red, green, blue,alpha);
#else
            Gl.glColor4f(red, green, blue, alpha);
#endif
        }



        static List<int> buffersARB = new List<int>();
        public static void GlDeleteBuffersARB(int name)
        {
            lock (buffersARB)
            {
                buffersARB.Add(name);
            }
        }
        public static void GlDeleteBuffersARB(int[] names)
        {
            lock (buffersARB)
            {
                buffersARB.AddRange(names);
            }
        }
        public static void DoGlDeleteBuffersARB()
        {
            lock (buffersARB)
            {
                if(buffersARB.Count > 0)
                {
                    int[] array = buffersARB.ToArray();
                    Gl.glDeleteBuffersARB(array.Length, array);
                    buffersARB.Clear();
                }
            }
        }
    }
}