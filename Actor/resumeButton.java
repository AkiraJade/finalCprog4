import greenfoot.*;

public class resumeButton extends Actor
{
    public resumeButton() {
        GreenfootImage img = getImage();
        img.scale(150, 50);
        setImage(img);
    }
    
    public void act()
    {
        if (Greenfoot.mouseClicked(this))
        {
            World world = getWorld();
            
            // Remove all pause menu elements
            world.removeObject(this);
            
            pauseBG overlay = (pauseBG) world.getObjects(pauseBG.class).get(0);
            world.removeObject(overlay);
            
            java.util.List<retryButton> retryBtns = world.getObjects(retryButton.class);
            if (!retryBtns.isEmpty()) world.removeObject(retryBtns.get(0));
            
            java.util.List<pexitButton> exitBtns = world.getObjects(pexitButton.class);
            if (!exitBtns.isEmpty()) world.removeObject(exitBtns.get(0));
            
            // Unpause
            java.util.List<pauseBtn> pauseBtns = world.getObjects(pauseBtn.class);
            if (!pauseBtns.isEmpty()) pauseBtns.get(0).resumeGame();
        }
    }
}