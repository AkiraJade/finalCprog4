import greenfoot.*;

public class pexitButton extends Actor
{
    public pexitButton() {
        GreenfootImage img = getImage();
        img.scale(150, 50);
        setImage(img);
    }
    
    public void act()
    {
        if (Greenfoot.mouseClicked(this))
        {
            Greenfoot.setWorld(new amainMenu());
        }
    }
}