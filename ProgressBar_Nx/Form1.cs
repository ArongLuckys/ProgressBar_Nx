using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgressBar_Nx
{
	public partial class Form1 : Form
	{
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		[DllImport("user32.dll")]
		private static extern int GetWindowRect(IntPtr hwnd, out Rect lpRect);

		public struct Rect
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		public IntPtr nx;
		public IntPtr nx2;
		public Rect rect;

		public Form1(IntPtr intPtr)
		{
			InitializeComponent();
			nx = intPtr;
			this.Location = new Point(0, 0);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			nx2 = FindWindowEx(nx,IntPtr.Zero, "MDIClient", null);

			SetParent(this.Handle, nx2);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (nx2 != IntPtr.Zero)
			{
				GetWindowRect(nx2,out rect);
				this.Size = new Size(rect.Right - rect.Left,30);
			}
			
		}

		private void killToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			if (progressBar1.Value != 100)
			{
				progressBar1.PerformStep();
			}
			else
			{
				progressBar1.Value = 0;
			}
		}
	}
}
