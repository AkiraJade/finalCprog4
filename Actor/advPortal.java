import greenfoot.*;
public class advPortal extends Actor
{
    private int levelNumber;
    private boolean isLocked;
    
    public advPortal(int levelNumber) {
        this.levelNumber = levelNumber;
        updateImage();
    }
    
    private void updateImage() {
        isLocked = (levelNumber > 1 && !LevelProgress.isAdvLevelComplete(levelNumber - 1));
        
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
            case 1: levelWorld = new pAdvLevel1(); break;
            case 2: levelWorld = new pAdvLevel2(); break;
            case 3: levelWorld = new pAdvLevel3(); break;
            case 4: levelWorld = new pAdvLevel4(); break;
            case 5: levelWorld = new pAdvLevel5(); break;
        }
        
        if (levelWorld != null) {
            Greenfoot.setWorld(levelWorld);
        }
    }
}