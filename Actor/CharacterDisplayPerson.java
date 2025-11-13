import greenfoot.*;

public class CharacterDisplayPerson extends Actor
{
    public CharacterDisplayPerson()
    {
        GreenfootImage img = new GreenfootImage("idle (1).png");
        img.scale(img.getWidth() * 3, img.getHeight() * 3); // Make 3x bigger
        setImage(img);
    }
    
    public void act()
    {
        // Do nothing - just display
    }
}