namespace CsCheckTests;

using CsCheck;
using GildedRose;

public class PropertyBasedTestApproach {
    [Fact]
    public void Example_ReducesNormalItemQualityBy1()
    {
        (from name in Gen.String
         from sellin in Gen.Int.Positive // by generating test data automatically, we're forced to recognise other constraints in our tests
         select new Item { Name = name, Quality = 10, SellIn = sellin })
        .Sample(i =>
        {
            var service = new GildedRose(new List<Item> { i });
            service.UpdateQuality();
            Assert.Equal(9, i.Quality);
        });
    }

    [Fact]
    public void Property_ReducesNormalItemQualityBy1()
    {
        (from name in Gen.String
         from quality in Gen.Int.Positive
         from sellin in Gen.Int.Positive
         select new Item { Name = name, Quality = 10, SellIn = sellin })
        .Sample(i =>
        {
            var previousQuality = i.Quality;
            var service = new GildedRose(new List<Item> { i });
            service.UpdateQuality();
            Assert.Equal(previousQuality - 1, i.Quality);
        });
    }

    [Fact(Skip = "Demo: This copy-pasted test fails correctly as we've not updated the assertion to check the SellIn property")]
    public void Bug_Example_ReducesNormalItemSellInBy1()
    {
        (from name in Gen.String
         from quality in Gen.Int
         select new Item { Name = name, Quality = quality, SellIn = 10 })
        .Sample(i =>
        {
            var service = new GildedRose(new List<Item> { i });
            service.UpdateQuality();
            Assert.Equal(9, i.Quality); // Fails here
        });
    }

    [Fact]
    public void Fix_Example_ReducesNormalItemSellInBy1()
    {
        (from name in Gen.String
         from quality in Gen.Int.Positive
         select new Item { Name = name, Quality = quality, SellIn = 10 })
        .Sample(i =>
        {
            var service = new GildedRose(new List<Item> { i });
            service.UpdateQuality();
            Assert.Equal(9, i.SellIn);
        });
    }

    [Fact]
    public void Fix_Property_ReducesNormalItemSellInBy1()
    {
        (from name in Gen.String
         from quality in Gen.Int.Positive
         from sellin in Gen.Int.Positive
         select new Item { Name = name, Quality = quality, SellIn = sellin })
        .Sample(i =>
        {
            var previousSellIn = i.SellIn;
            var service = new GildedRose(new List<Item> { i });
            service.UpdateQuality();
            Assert.Equal(previousSellIn - 1, i.SellIn);
        });
    }
}