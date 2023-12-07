using DeacomDutiesExercise.Utils;

namespace UnitTests
{
    public class ExtensionMethodsInt_Test
    {
        [Fact]
        public void GetReadableTime_Test()
        {
            int a = 78;
            int b = 94051;
            int c = 3667;

            Assert.Equal("00:01:18", a.GetReadableTime());
            Assert.Equal("26:07:31", b.GetReadableTime());
            Assert.Equal("01:01:07", c.GetReadableTime());
        }

        [Fact]
        public void Calculate_3OR5_Test()
        {
            Assert.Equal(3, 4.Calculate_3OR5());
            Assert.Equal(9168, 200.Calculate_3OR5());
            Assert.Equal(23, 10.Calculate_3OR5());
        }
    }
}