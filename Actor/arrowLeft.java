import greenfoot.*;

public class arrowLeft extends Actor
{
    private GreenfootImage normalImg;
    private GreenfootImage hoverImg;

    public arrowLeft() {
        normalImg = new GreenfootImage(getImage());
        normalImg.scale(60, 60);

        hoverImg = new GreenfootImage(normalImg);
        hoverImg.setTransparency(180); // Slight highlight on hover

        setImage(normalImg);
    }
    
    public void act()
    {
        // Hover Effect
        if (Greenfoot.mouseMoved(this)) {
            setImage(hoverImg);
        } else if (Greenfoot.mouseMoved(null)) {
            setImage(normalImg);
        }

        // Click to change character
        if (Greenfoot.mouseClicked(this)) {
            if (getWorld() instanceof charSelectionBg) {
                ((charSelectionBg)getWorld()).cycleLeft();
                
            }
        }
    }
}
