import greenfoot.*;

public class pauseBtn extends Actor
{
    private boolean isPaused = false;
    
    public pauseBtn() {
        GreenfootImage img = getImage();
        img.scale(50, 50);
        setImage(img);
    }
    
    public void act()
    {
        if (Greenfoot.mouseClicked(this) && !isPaused)
        {
            pauseGame();
        }
    }
    
    private void pauseGame()
    {
        isPaused = true;
        World world = getWorld();
        
        // Add pause overlay
        pauseBG overlay = new pauseBG();
        world.addObject(overlay, 375, 200);
        
        // Add buttons on top of overlay
        resumeButton resumeBtn = new resumeButton();
        world.addObject(resumeBtn, 375, 180);
        
        retryButton retryBtn = new retryButton();
        world.addObject(retryBtn, 375, 250);
        
        pexitButton exitBtn = new pexitButton();
        world.addObject(exitBtn, 375, 320);
    }
    
    public void resumeGame()
    {
        isPaused = false;
    }
    
    public boolean getIsPaused()
    {
        return isPaused;
    }
}