import greenfoot.*;

public class arrowRight extends Actor
{
    private GreenfootImage normalImg;
    private GreenfootImage hoverImg;

    public arrowRight() {
        normalImg = new GreenfootImage(getImage());
        normalImg.scale(60, 60);
        normalImg.mirrorHorizontally(); // Point right

        hoverImg = new GreenfootImage(normalImg);
        hoverImg.setTransparency(180);

        setImage(normalImg);
    }
    
    public void act()
    {
        if (Greenfoot.mouseMoved(this)) {
            setImage(hoverImg);
        } else if (Greenfoot.mouseMoved(null)) {
            setImage(normalImg);
        }

        if (Greenfoot.mouseClicked(this)) {
            if (getWorld() instanceof charSelectionBg) {
                ((charSelectionBg)getWorld()).cycleRight();
            }
        }
    }
}
