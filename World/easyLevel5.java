import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class easyLevel5 here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class easyLevel5 extends World
{

    /**
     * Constructor for objects of class easyLevel5.
     * 
     */
    
    Platform fake1;
    int playerX;
    int playerY;
    Platform hidden;
    
    public easyLevel5()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(750, 400, 1); 
        prepare();
    }
    
    public void act(){
        platformUp();
        platformMove();
    }
    public void prepare() {
        Person  player1 = new Person();
        addObject(player1, 30, 280);
        Platform p1 = new Platform (350,100);
        addObject(p1, 170, 350);
        fake1 = new Platform (25,160);
        addObject(fake1, 332, 390);
        Platform p2 = new Platform (100, 100);
        addObject(p2, 450, 350);
 
        Platform p3 = new Platform(180,100);
        addObject(p3, 660, 350);
        
        easyPortal ePor = new easyPortal(5);
        addObject(ePor, 704, 270);
        pauseBtn pauseButton = new pauseBtn();
        addObject(pauseButton, 700, 30);
    }
    
    boolean moveUp = false;
    boolean didJump = false;
    boolean moveSide2 = false;
    boolean moveDown = false;
    boolean oneTime = false;
    boolean hiddenCreated = false;
    
    private void platformUp() {
        java.util.List<Person>players = getObjects(Person.class);
        
        if(players.isEmpty()){
            return;
        }
        
        Person player = players.get(0);
        playerX = player.getX();
        playerY = player.getY();
        if (playerX >= 280 && playerX < 400 && playerY <= 190 && fake1.getY() >= 240 
        && !oneTime) {
           moveUp = true;
           didJump = true;
           
           Spike sp1 = new Spike();
           addObject(sp1, 298, 292);
        }
        
        if (moveUp) {
             fake1.setLocation(fake1.getX(), fake1.getY() - 40);
             
        }
        
        if(fake1.getY() <= 240) {
            moveUp = false;
            oneTime = true;
        }
        
        
    }
    
    public void platformMove() {
        
        if(!didJump && !hiddenCreated){
        hidden = new Platform(60,100);
        addObject(hidden, 370, 399);
        hidden.getImage().setTransparency(0);
        hiddenCreated = true;
        }
        
        if(didJump && hidden != null) {
            removeObject(hidden);
            hidden = null;
        }
        if(playerX >= 390 && didJump && fake1.getX() <= 475) {
            moveSide2 = true; 
           
        } 
        
        
        
        if (moveSide2) {
            fake1.setLocation(fake1.getX() + 25, fake1.getY());
        }
        
        if (fake1.getX() >= 475){
            moveSide2 = false;
            moveDown = true;
        }
        
        if(moveDown) {
             fake1.setLocation(fake1.getX(), fake1.getY() + 10);
        }
        
        if(fake1.getY() >= 399){
            moveDown = false;
        }
    }
    
    
}