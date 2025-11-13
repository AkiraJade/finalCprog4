import greenfoot.*;

public class mainMenuPlay extends Actor {

    public mainMenuPlay() {
        GreenfootImage img = getImage(); 
        img.scale(170, 50); 
        setImage(img);
    }
    
    public void act()
    {
        if (Greenfoot.mouseClicked(this))
        {
            Greenfoot.setWorld(new amainMenuP());
        }
    }
}
