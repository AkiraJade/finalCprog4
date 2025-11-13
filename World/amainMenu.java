import greenfoot.*;
public class amainMenu extends World {
    public amainMenu() {    
        super(750, 400, 1); 
        prepare();
    }
    private void prepare() {
        GreenfootImage bg = new GreenfootImage("mainMenuBg.png"); 
        bg.scale(750, 400);
        setBackground(bg);
        title titleSc = new title();
        addObject(titleSc, 380, 100);
        mainMenuPlay playBtn = new mainMenuPlay();
        addObject(playBtn, 370, 200);
        mainMenuChar charBtn = new mainMenuChar();
        addObject(charBtn, 370, 260);
        mainMenuAbout aboutBtn = new mainMenuAbout();
        addObject(aboutBtn, 370, 320);
        
    }
}