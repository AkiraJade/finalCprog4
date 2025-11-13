import greenfoot.*;

public class CharacterDisplayPerson3 extends Actor
{
    public CharacterDisplayPerson3()
    {
        GreenfootImage img = new GreenfootImage("boy1.png");
        img.scale(img.getWidth() * 3, img.getHeight() * 3); // Make 3x bigger
        setImage(img);
    }
    
    public void act()
    {
        // Do nothing - just display
    }
}