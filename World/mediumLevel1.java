import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class mediumLevel1 here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class mediumLevel1 extends World
{

    /**
     * Constructor for objects of class mediumLevel1.
     * 
     */
    
    int playerX;
    int playerY;
    Platform p2Down;
    boolean spikeAppeared = false;
    Spike sp1;
    Platform p4Up;
    Platform p5Push;
    Platform p6Disappearing;
    Platform p7MovingPlatform;
    public mediumLevel1()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
         super(750, 400, 1); 
        
     

   
         prepare();
    }
    
    public void prepare(){
        Person  player1 = new Person();
        addObject(player1, 30, 280);
        Platform p1 = new Platform (180,100);
        addObject(p1, 80, 350);
        p2Down = new Platform (100,100);
        addObject(p2Down, 220, 350);
        Platform p3 = new Platform (180,100);
        addObject(p3, 360, 350);
        p4Up = new Platform (25,150);
        addObject(p4Up, 282, 390);
        p5Push = new Platform(25,150);
        addObject(p5Push, 437, 390);
        p7MovingPlatform = new Platform (70,20);
        addObject(p7MovingPlatform, 410, 310);//437
        mediumPortal mPor = new mediumPortal(1);
        addObject(mPor, 704, 270);
        pauseBtn pauseButton = new pauseBtn();
        addObject(pauseButton, 700, 30);
    }
    
    public void act(){
        moveDown();
        spikeAppear();
        trapsAppear();
        trapsAppearV2();
        trapsAppearV3();
    }
    
    boolean moveP2 = false;
    boolean moveP4 = false;
    boolean oneTimeP4 = false;
    public void moveDown(){
        java.util.List<Person>players = getObjects(Person.class);
        
        if(players.isEmpty()){
            return;
        }
        
        Person player = players.get(0);
        playerX = player.getX();
        playerY = player.getY();
        if(playerX >= 170 && !moveP2 && !stayP2Up){
            moveP2 = true;
        }
        
        if (moveP2){
            p2Down.setLocation(p2Down.getX(), p2Down.getY() + 5);
        }
        
        if(p2Down.getY() >= 399){
            moveP2 = false;
        }
          
    }
    
    public void spikeAppear(){
        if(playerX >= 270 && !spikeAppeared) {
            sp1 = new Spike();
            addObject(sp1, 288, 292);
            spikeAppeared = true;
        }
        
        if (spikeAppeared && playerX <= 264){
            removeObject(sp1);
        }
    }
    boolean p2Up = false;
    boolean allowP4Up = false;
    boolean stayP2Up = false;
    
    public void trapsAppear(){
        
        if(playerY >= 330 && spikeAppeared){
            allowP4Up = true;
        }
        if (playerX >= 250 && playerY <= 240 && spikeAppeared && allowP4Up && !oneTimeP4){
            moveP4 = true;
        } 
        
        if(allowP4Up && playerX <= 170){
            
            p2Up = true;
            allowP4Up = false;
        }
        
        
        if (moveP4){
            p4Up.setLocation(p4Up.getX(), p4Up.getY() - 40);
        }
        
        if(p4Up.getY() <= 240){
            moveP4 = false;
            oneTimeP4 = true;
            
            Spike sp2 = new Spike();
            addObject(sp2, 250, 341);
        }
        
        if (p2Up && playerX <= 170){
            p2Down.setLocation(p2Down.getX(), p2Down.getY() - 10);
        }
        
        if(p2Down.getY() <= 300){
            p2Up = false;
            stayP2Up = true;
        }
    }
     boolean moveP2Right = false;
     boolean p2OneTimeRight = false;
     boolean moveP5Up = false;
     boolean p5OneTimeUp = false;
     boolean p5OneTimePush = false;
    public void trapsAppearV2(){
        if(playerX >= 340 && !p2OneTimeRight){
            moveP2Right = true; //340
        }
        
        if(moveP2Right){
            p2Down.setLocation(p2Down.getX() + 10, p2Down.getY());
        }
        
        if(p2Down.getX() >= 320){
            moveP2Right = false;
            p2OneTimeRight = true;
            
            p2Down.setLocation(p2Down.getX(), p2Down.getY() + 10);
        }
        
        if (p2OneTimeRight && playerX >= 400 && !p5OneTimeUp){
            moveP5Up = true;
            
        }
        
        if(moveP5Up) {
            p5Push.setLocation(p5Push.getX(), p5Push.getY() - 20);
            
        }
        
        if(p5Push.getY() <= 250 && !p5OneTimePush) {
            moveP5Up = false;
            p5OneTimeUp = true;
            
            p5Push.setLocation(p5Push.getX() - 25, p5Push.getY());
            
           
        }
        
        if(p5Push.getX() <= 295){
             p5OneTimePush = true;
             //440
             p5Push.setLocation(p5Push.getX(), p5Push.getY() + 10);
        }
    }
    
    boolean p6Appear = false;
    boolean p6Disappeared = false;
    boolean startMoving = false;
    boolean secondAppear = false;
    
    public void trapsAppearV3(){
        if(playerX >= 440 && !p6Appear) {
        
            if (!secondAppear){
        p6Disappearing = new Platform(260, 100);
        addObject(p6Disappearing, 635, 350);
    } else {
         p6Disappearing = new Platform(150, 100);
        addObject(p6Disappearing, 690, 350);
    }
        p6Appear = true;
        }
        
        if(p6Appear && playerX >= 460 && p7MovingPlatform.getX() <= 590){
            removeObject(p6Disappearing);
            p6Disappeared = true;
           
        }
        
        if (p6Disappeared && playerX >= 440 && playerY >= 281){
            startMoving = true;
        }
        if(startMoving){
            p7MovingPlatform.setLocation(p7MovingPlatform.getX() + 2, 
            p7MovingPlatform.getY());
        }
        
        if(p7MovingPlatform.getX() >= 590){
            startMoving = false;
            p6Disappeared = false;
            p6Appear = false;
            secondAppear = true;
            
            p7MovingPlatform.setLocation(p7MovingPlatform.getX(),
            p7MovingPlatform.getY() + 10);
        }
        
        if(p7MovingPlatform.getY() >= 399){
              p7MovingPlatform.setLocation(p7MovingPlatform.getX() + 5,
            p7MovingPlatform.getY());
            
            Spike sp = new Spike();
            addObject(sp, 630, 292);
        }
    }
    
    
}