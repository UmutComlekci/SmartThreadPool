using System;
using Amib.Threading;
using NUnit.Framework;

namespace SmartThreadPoolTests
{
	/// <summary>
	/// Summary description for TestExceptions.
	/// </summary>
	[TestFixture]
	[Category("TestExceptions")]
	public class TestExceptions
	{
		private class DivArgs
		{
			public int x;
			public int y;
		}

		[Test]
		public void ExceptionThrowing() 
		{ 
			SmartThreadPool _smartThreadPool = new SmartThreadPool();

			DivArgs divArgs = new DivArgs();
			divArgs.x = 10;
			divArgs.y = 0;

			IWorkItemResult wir = 
				_smartThreadPool.QueueWorkItem(new WorkItemCallback(this.DoDiv), divArgs);

			try
			{
				wir.GetResult();
			}
			catch(WorkItemResultException wire)
			{
				Assert.IsTrue(wire.InnerException is DivideByZeroException);
				return;
			}
			catch(Exception e)
			{
                e.GetHashCode();
				Assert.Fail();
			}
			Assert.Fail();
		} 

		[Test]
		public void ExceptionReturning() 
		{ 
			bool success = true;

			SmartThreadPool _smartThreadPool = new SmartThreadPool();

			DivArgs divArgs = new DivArgs();
			divArgs.x = 10;
			divArgs.y = 0;

			IWorkItemResult wir = 
				_smartThreadPool.QueueWorkItem(new WorkItemCallback(this.DoDiv), divArgs);

			Exception e = null;
			try
			{
				wir.GetResult(out e);
			}
			catch (Exception ex)
			{
                ex.GetHashCode();
				success = false;
			}

			Assert.IsTrue(success);
			Assert.IsTrue(e is DivideByZeroException);
		}

        private object DoDiv(object state)
        {
            DivArgs divArgs = (DivArgs)state;
            return (divArgs.x / divArgs.y);
        }

        [Test]
		public void ExceptionType()
		{ 
			SmartThreadPool smartThreadPool = new SmartThreadPool();

	        var workItemResult = smartThreadPool.QueueWorkItem(new Func<int>(ExceptionMethod));

            smartThreadPool.WaitForIdle();

            Assert.IsInstanceOf<NotImplementedException>(workItemResult.Exception);

            smartThreadPool.Shutdown();
        }

	    public int ExceptionMethod()
        {
            throw new NotImplementedException();
        }
	}
}
