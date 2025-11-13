import greenfoot.*;

public class mediumPortal extends Actor
{
    private int levelNumber;
    private boolean isLocked;
    
    public mediumPortal(int levelNumber) {
        this.levelNumber = levelNumber;
        updateImage();
    }
    
    private void updateImage() {
        isLocked = (levelNumber > 1 && !LevelProgress.isMediumLevelComplete(levelNumber - 1));
        
        GreenfootImage img = getImage(); 
        img.scale(80, 80);
        
        if (isLocked) {
            img.setTransparency(100);
        }
        
        setImage(img);
    }
    
    public void act()
    {
        updateImage();
        
        if (Greenfoot.mouseClicked(this))
        {
            if (isLocked) {
                getWorld().showText("Complete previous level first!", getWorld().getWidth()/2, 50);
            } else {
                goToLevel();
            }
        }
    }
    
    private void goToLevel() {
        World levelWorld = null;
        
        switch(levelNumber) {
            case 1: levelWorld = new mediumLevel1(); break;
            case 2: levelWorld = new mediumLevel2(); break;
            case 3: levelWorld = new mediumLevel3(); break;
            case 4: levelWorld = new mediumLevel4(); break;
            case 5: levelWorld = new mediumLevel5(); break;
        }
        
        if (levelWorld != null) {
            Greenfoot.setWorld(levelWorld);
        }
    }
}