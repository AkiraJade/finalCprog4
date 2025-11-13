import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)
import java.util.ArrayList;
/**
 * Write a description of class ohardLevel4 here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class ohardLevel4 extends World
{

    /**
     * Constructor for objects of class ohardLevel4.
     * 
     */
     enum PlatformState {
        WAITING,      // Platform hasn't started moving yet
        MOVING,       // Platform is moving up
        STOPPED,      // Platform stopped at target position
        REMOVED,       // Platform has been removed from world
        DISAPPEAR,
        SPAWNED,
        APPEAR
    }
    
    enum SpikeState {
        NOT_CREATED,  // Spike doesn't exist yet
        CREATED,      // Spike exists but not ready to move
        READY,        // Player touched ground, spike ready to move
        MOVING,       // Spike is moving right
        STOPPED,       // Spike finished moving
        DISAPPEAR,
        APPEAR
    }
    
    int playerX;
    int playerY;
    Spike sp1;
    Spike sp2;
    Spike sp3;
    Spike sp4Down;
    ArrayList<Spike> sp4DownSpikes = new ArrayList<Spike>();
    int frameCount = 0;
    int frameTarget = 60;
    SpikeState sp1State = SpikeState.READY;
    SpikeState sp2State = SpikeState.READY;
    SpikeState sp3State = SpikeState.READY;
    Platform p3Up;
    Platform p4Up;
    Platform p6Down;
    PlatformState p3UpState = PlatformState.WAITING;
    PlatformState p4UpState = PlatformState.WAITING;
    PlatformState p6DownState = PlatformState.WAITING;
    public ohardLevel4()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
         super(750, 400, 1); 
         prepare();
    }
    
    public void prepare(){
         Person  player1 = new Person();
         addObject(player1, 30, 280);
        Platform p1 = new Platform (200,100);
        addObject(p1, 100, 350);
        sp1 = new Spike();
        addObject(sp1, 120, 292);
        FakeSpike fp1 = new FakeSpike();
        addObject(fp1, 185, 292);
           Platform p2 = new Platform (250,100);
        addObject(p2, 380, 350);
        Spike sp3 = new Spike();
        sp3 = new Spike();
        addObject(sp3, 380, 292);
        p3Up = new Platform(25,150);
        addObject(p3Up, 492, 399);
        Platform p4 = new Platform(100, 100);
        addObject(p4, 590, 350);
        p4Up = new Platform(25,150);
        addObject(p4Up, 552, 399);
        Platform p5 = new Platform(100,100);
        addObject(p5, 725, 350);
        p6Down = new Platform(800, 100);
        addObject(p6Down, 400, 0);
        createSpikeRow(25, 57, 15,50, 15, true);
        hardPortal hPor = new hardPortal(4);
        addObject(hPor, 725, 263);
        
        pauseBtn pauseButton = new pauseBtn();
        addObject(pauseButton, 700, 30);
    }
    
   public void createSpikeRow(int startX, int y, int count, int width, int height, boolean pointDown) {
    for(int i = 0; i < count; i++) {
        sp4Down = new Spike();
        GreenfootImage sp4Image = sp4Down.getImage();
        sp4Image.scale(width, height);
        
        if(pointDown) {
            sp4Image.rotate(180);
        }
        
        addObject(sp4Down, startX + (i * 50), y);
        sp4DownSpikes.add(sp4Down);
    }
}

    public void act(){
        updatePlayerPosition();
        platformDown();
        trapsV1();
        trapsV2();
        trapsV3();
    }
    
    public void updatePlayerPosition(){
        java.util.List<Person> players = getObjects(Person.class);
        
        if(players.isEmpty()){
            return;
        }
        
        Person player = players.get(0);
        playerX = player.getX();
        playerY = player.getY();
    }
    
    public void platformDown(){
        
        if(frameCount <= frameTarget){
          
            frameCount++;
            if(frameCount == frameTarget){
                  p6Down.setLocation(p6Down.getX(),p6Down.getY() + 10);
                 for(Spike sp4Down : sp4DownSpikes) {
                sp4Down.setLocation(sp4Down.getX(), sp4Down.getY() + 10);
            }
                frameCount = 0;
            }
        }
    }
    public void trapsV1(){
        if(playerX >= 100 && playerY <= 215
        && sp1State == SpikeState.READY){
            sp1State = SpikeState.MOVING;
        } else if(sp1State == SpikeState.MOVING){
            sp1.setLocation(sp1.getX() + 5, sp1.getY());
            if(sp1.getX() >= 155){
                sp1State = SpikeState.STOPPED;
            }
        }
        
        if(playerX >= 290 && sp2State == SpikeState.READY){
            sp2State = SpikeState.APPEAR;
        } else if(sp2State == SpikeState.APPEAR){
            sp2 = new Spike();
        addObject(sp2, 320, 292);
        }
    }
    
    boolean firstMove = false;
    public void trapsV2(){
        if(playerX >= 410 && p3UpState == PlatformState.WAITING){
            p3UpState = PlatformState.MOVING;
        } else if(p3UpState == PlatformState.MOVING && !firstMove
        && p3Up.getX() > 420) {
            p3Up.setLocation(p3Up.getX(), p3Up.getY() - 10);
            if(p3Up.getY() <= 250){
                p3UpState = PlatformState.STOPPED;
                firstMove = true;
            }
        } else if(p3UpState == PlatformState.STOPPED && firstMove){
            p3UpState = PlatformState.MOVING;
        } else if(p3UpState == PlatformState.MOVING && firstMove){
            p3Up.setLocation(p3Up.getX() - 20, p3Up.getY());
            if(p3Up.getX() <= 420){
                p3UpState = PlatformState.STOPPED;
                  firstMove = false;
            }
        } else if(playerX <= 360 
        && p3UpState == PlatformState.STOPPED && p3Up.getX() <= 420){
            p3UpState = PlatformState.MOVING;
        } else if (p3UpState == PlatformState.MOVING && p3Up.getX() <= 420){
              p3Up.setLocation(p3Up.getX(), p3Up.getY() + 20);
            if(p3Up.getY() >= 399){
                p3UpState = PlatformState.STOPPED;
               
            }
        }
    }
    
   
    public void trapsV3(){
        if(playerX >= 515 && playerY <= 270
        && p4UpState == PlatformState.WAITING){
            p4UpState = PlatformState.MOVING;
        } else if (p4UpState == PlatformState.MOVING){
            p4Up.setLocation(p4Up.getX(), p4Up.getY() - 20);
            if(p4Up.getY() <= 250){
                p4UpState = PlatformState.STOPPED;
               
            }
        } else {
             p4Up.setLocation(p4Up.getX(), p4Up.getY() + 5);
            
        }
        
        if(playerX >= 660 && playerY <= 190
        && sp3State == SpikeState.READY){
            sp3State = SpikeState.APPEAR;
        } else if(sp3State == SpikeState.APPEAR){
            sp3 = new Spike();
            addObject(sp3, 690, 292);
            sp3State = SpikeState.DISAPPEAR;
        } else if (sp3State == SpikeState.DISAPPEAR
        && playerX <= 650 && playerY <= 282) {
            removeObject(sp3);
        }
        
    }
    
    //640 281
}