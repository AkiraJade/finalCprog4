import greenfoot.*;

public class mainMenuChar extends Actor
{
    public mainMenuChar() {
        GreenfootImage img = getImage(); 
        img.scale(170, 50); 
        setImage(img);
    }
    
    public void act()
    {
        if (Greenfoot.mouseClicked(this))
        {
            Greenfoot.setWorld(new charSelectionBg());
        }
    }
}