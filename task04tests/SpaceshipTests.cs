namespace task04tests;

using Xunit;
using Moq;
using task04;

public class SpaceshipTests
{
    [Fact]
    public void Cruiser_ShouldHaveCorrectStats()
    {
        ISpaceship cruiser = new Cruiser();
        Assert.Equal(50, cruiser.Speed);
        Assert.Equal(10, cruiser.FirePower);
    }

    [Fact]
    public void Fighter_ShouldHaveCorrectStats()
    {
        ISpaceship fighter = new Fighter();
        Assert.Equal(100, fighter.Speed);
        Assert.Equal(5, fighter.FirePower);
    }

    [Fact]
    public void Fighter_ShouldBeFasterThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.Speed > cruiser.Speed);
    }

    [Fact]
    public void Fighter_ShouldDealLessDamageThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.FirePower < cruiser.FirePower);
    }

    [Fact]
    public void Cruiser_MovingForwardIsCorrect()
    {
        var cruiser = new Cruiser();
        cruiser.Rotate(90);
        cruiser.MoveForward();

        Assert.Equal(0, cruiser.x);
        Assert.Equal(10, cruiser.y);
    }

    [Fact]
    public void Fighter_MovingForwardIsCorrect()
    {
        var fighter = new Fighter();
        fighter.Rotate(180);
        fighter.MoveForward();

        Assert.Equal(-10, fighter.x);
        Assert.Equal(0, fighter.y);
    }

    [Fact]
    public void Cruiser_RotatesCorrectly()
    {
        var cruiser = new Cruiser();
        cruiser.Rotate(370);

        Assert.Equal(10, cruiser.angle);
    }

    [Fact]
    public void Fighter_RotatesCorrectly()
    {
        var fighter = new Fighter();
        fighter.Rotate(370);

        Assert.Equal(10, fighter.angle);
    }
}