import greenfoot.*;

public class easyPortal extends Actor
{
    private int levelNumber;
    private boolean isLocked;
    
    public easyPortal(int levelNumber) {
        this.levelNumber = levelNumber;
        updateImage();
    }
    
    private void updateImage() {
        // Check if level is locked
        isLocked = (levelNumber > 1 && !LevelProgress.isEasyLevelComplete(levelNumber - 1));
        
        GreenfootImage img = getImage(); 
        img.scale(80, 80);
        
        // If locked, make it darker/grayed out
        if (isLocked) {
            img.setTransparency(100); // Make it semi-transparent
        }
        
        setImage(img);
    }
    
    public void act()
    {
        updateImage(); // Update lock status every frame
        
        if (Greenfoot.mouseClicked(this))
        {
            if (isLocked) {
                // Show a message that level is locked
                getWorld().showText("Complete previous level first!", getWorld().getWidth()/2, 50);
            } else {
                goToLevel();
            }
        }
    }
    
    private void goToLevel() {
        World levelWorld = null;
        
        switch(levelNumber) {
            case 1: levelWorld = new easyLevel1(); break;
            case 2: levelWorld = new easyLevel2(); break;
            case 3: levelWorld = new easyLevel3(); break;
            case 4: levelWorld = new easyLevel4(); break;
            case 5: levelWorld = new easyLevel5(); break;
        }
        
        if (levelWorld != null) {
            Greenfoot.setWorld(levelWorld);
        }
    }
}